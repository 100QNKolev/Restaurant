using Microsoft.AspNetCore.Mvc;
using Restaurant.Data;
using Restaurant.Models;
using System.Diagnostics;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            int tablesCount = _context.Tables
                .Count();
			int customersCount = _context.Customers
				.Count();
            int finishedReservations = _context.Reservations
                .Where(r => DateTime.Now > r.End)
                .Count();
            int upcomingReservations = _context.Reservations
				.Where(r => r.Start > DateTime.Now || DateTime.Now < r.End)
				.Count();

			ViewData["TablesCount"] = tablesCount;
			ViewData["CustomersCount"] = customersCount;
			ViewData["FinishedReservations"] = finishedReservations;
			ViewData["UpcomingReservations"] = upcomingReservations;

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
