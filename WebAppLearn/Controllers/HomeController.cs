using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebAppLearn.Models;

namespace WebAppLearn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Connexion()
        {
            return View();
        }

        public IActionResult Inscription()
        {
            return View();
        }
        public IActionResult InscriptionPost()
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