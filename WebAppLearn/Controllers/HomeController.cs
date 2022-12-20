using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
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

        public IActionResult ConnexionPost()
        {
            return View();
        }

        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InscriptionPost()
        {
            UsersModel user = new UsersModel();
            user.Email = HttpContext.Request.Form["Email"].ToString();
            user.Nom = HttpContext.Request.Form["Nom"].ToString();
            user.Prenom = HttpContext.Request.Form["Prenom"].ToString();
            user.Password = HttpContext.Request.Form["Password"].ToString();
            var Password2 = HttpContext.Request.Form["Password2"].ToString();
            if (user.Email != "" && user.Nom != "" && user.Prenom != "" && user.Password != "" && user.Password == Password2)
            {
                SqlConnection con = new SqlConnection(GetConString.ConString());
                string query = "INSERT INTO Users(Email, Nom, Prenom, Password) values ('" + user.Email + "','" + user.Nom + "','" + user.Prenom + "','" + user.Password + "')";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    ViewBag.Result = "Inscription réussit";
                }
                else
                {
                    ViewBag.Result = "Echec de l'inscription";
                }
                return View("Connexion");
            }
            else
            {
                return View("error");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}