using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Models;
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

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
