using Microsoft.AspNetCore.Mvc;

namespace WebAppLearn.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
