using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;
using System.Globalization;
using System.Security.Claims;

namespace Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantController(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> All() 
        {
            var tables = await _context.Tables
                .AsNoTracking()
                .Select(t => new TableInfoViewModel
                {
                    Id = t.Id,
                    NumberOfSeats = t.NumberOfSeats,
                    IsSmokingAllowed = t.IsSmokingAllowed
                })
                .ToListAsync();

            return View(tables);
        }

        [HttpGet]
        public async Task<IActionResult> Reservations()
        {
            var reservations = await _context.Reservations
                .Include(r => r.Table)
                .Include(r => r.Customer)
                .Select(r => new ReservationViewModel
                {
                   Id = r.Table.Id,
                   NumberOfSeats = r.Table.NumberOfSeats,
                   IsSmokingAllowed = r.Table.IsSmokingAllowed,
                   FirstName = r.Customer.FirstName,
                   LastName = r.Customer.LastName,
                   Start = r.Start.ToString("yyyy-MM-dd H:mm"),
                   End = r.End.ToString("yyyy-MM-dd H:mm")
                })
                .AsNoTracking()
                .ToListAsync();

            return View(reservations);
        }

        [HttpGet]
        public async Task<IActionResult> Add() 
        {
            var model = new CustomerFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CustomerFormViewModel model) 
        {
            var entity = new Customer 
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                TelephoneNumber = model.TelephoneNumber
            };

            await _context.Customers.AddAsync(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Reserve(int id) 
        {
            var model = new ReserveViewModel();

            model.TableId = id;

            model.Customers = await _context.Customers
                .AsNoTracking()
                .Select(c => new ReserveCustomerViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                })
                .ToListAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ReserveViewModel model ,int id)
        {
            var reservations = await _context.Reservations
                .Where(r => r.TableId == id).ToListAsync();

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            if (!DateTime.TryParseExact(
                model.Start,
                "yyyy-MM-dd H:mm",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out start))
            {
                ModelState.AddModelError(nameof(model.Start), "Invalid reservation time!");
            }

            if (!DateTime.TryParseExact(
               model.End,
               "yyyy-MM-dd H:mm",
               CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out end))
            {
                ModelState.AddModelError(nameof(model.End), "Invalid reservation time!");
            }

            foreach (var reservation in reservations)
            {
                bool overlap = (start < reservation.End) && (end > reservation.Start);

                if (overlap == true)
                {
                    ModelState.AddModelError(nameof(model.Start),"Taken reservation time!");
                }
            }

            if (!ModelState.IsValid)
            {
                model.Customers = await _context.Customers
               .AsNoTracking()
               .Select(c => new ReserveCustomerViewModel
               {
                   Id = c.Id,
                   FirstName = c.FirstName,
                   LastName = c.LastName
               })
               .ToListAsync();

                return View(model);
            }

            var entity = new Reservation 
            {
                CustomerId = model.CustomerId,
                TableId = model.TableId,
                Start = start,
                End = end,
                Description = model.Description,
            };

            await _context.Reservations.AddAsync(entity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

    }
}
