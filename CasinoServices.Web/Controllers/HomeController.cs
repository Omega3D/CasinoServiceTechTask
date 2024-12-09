using CasinoServices.Infrastracture.Services;
using CasinoServices.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CasinoServices.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MongoDBService _mongoDbService;

        public HomeController(ILogger<HomeController> logger, MongoDBService mongoDBService)
        {
            _logger = logger;
            _mongoDbService = mongoDBService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View(nameof(Privacy));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
