using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using WebAppLearn.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        [HttpPost]
        public IActionResult Connexion(UsersModel user)
        {
            return View();
        }


        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inscription(UsersModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                user.Email = HttpContext.Request.Form["Email"].ToString();
                user.Nom = HttpContext.Request.Form["Nom"].ToString();
                user.Prenom = HttpContext.Request.Form["Prenom"].ToString();
                user.Password = HttpContext.Request.Form["Password"].ToString();
                var password2 = HttpContext.Request.Form["Password2"].ToString();
                if (user.Email != "" && user.Nom != "" && user.Prenom != "" && user.Password != "" && user.Password == password2)
                {
                    try
                    {
                        String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RazorCalendar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {  
                            connection.Open();
                            String sql = "INSERT INTO dbo.Users (Email, Nom, Prenom, Password) VALUES (@email, @nom, @prenom, @password);";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@email", user.Email);
                                command.Parameters.AddWithValue("@nom", user.Nom);
                                command.Parameters.AddWithValue("@prenom", user.Prenom);
                                command.Parameters.AddWithValue("@password", user.Password);
                                command.ExecuteNonQuery();
                            }
                            // connection.Close();
                        }
                        ViewData["message"] = "Inscription effectué !";
                        return View("Connexion");
                    }
                    catch (Exception error)
                    {
                        ViewData["message"] = error.Message;
                        return View();
                    }
                    
                }
                else
                {
                    ViewData["message"] = "Echec de l'inscription !";
                    return View("inscription");
                }
            }

        }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}