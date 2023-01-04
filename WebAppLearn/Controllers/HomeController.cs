using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;
using WebAppLearn.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System;
using System.Drawing;

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
        [ValidateAntiForgeryToken]
        public IActionResult Connexion(UsersModel user)
        {

            // méthode avec hashage de mot de passe
            var passwordDB = "";
            string mail = HttpContext.Request.Form["Email"].ToString();
            string password = HttpContext.Request.Form["Password"].ToString();

            if (mail !="" && password != "")
            {

                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RazorCalendar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                SqlConnection connection = new SqlConnection(connectionString);

                string selectMail = "select Password from dbo.Users where Email='" + mail + "'";
                connection.Open();
                SqlCommand cmd = new SqlCommand(selectMail, connection);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        passwordDB = dr["Password"].ToString();
                    }
                }

                if (passwordDB !="") 
                {
                    PasswordHasher<string> pw = new PasswordHasher<string>();
                    var verificationResult = pw.VerifyHashedPassword(mail, passwordDB, password);
                    if (verificationResult == PasswordVerificationResult.Success)
                    {
                        // HttpContext.Session.SetInt32("Login", 1);
                        // HttpContext.Session.SetString("UserName", mail);
                        return View("./Calendar/Index");
                    }
                    else
                    {
                        // HttpContext.Session.SetInt32("Login", 0);
                        // HttpContext.Session.SetString("UserName", "");
                        ViewData["message"] = "Echec de la vérification du mot de passe !";
                        return View();
                    }
                }
                else
                {
                    ViewData["message"] = "Echec de la récuperation du mot de passe pour la comparaison";
                    return View();
                }
            }
            else
            {
                ViewData["message"] = "Tous les champs doivent être rempli !";
                return View();
            }

        // méthode sans hashage de mot de passe
        //String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RazorCalendar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //String queryString = "SELECT * FROM dbo.Users WHERE Email = @Email AND Password = @Password";
        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    SqlCommand command = new SqlCommand(queryString, connection);
        //    command.Parameters.Add("@Email", System.Data.SqlDbType.NChar, 100).Value = user.Email;
        //    command.Parameters.Add("@Password", System.Data.SqlDbType.NChar, 100).Value = user.Password;
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = command.ExecuteReader();
        //        if(reader.HasRows)
        //        {
        //            reader.Close();
        //            return View("../Calendar/index");
        //        }
        //        else
        //        {
        //            reader.Close();
        //            ViewData["message"] = "Echec de l'identification !";
        //            return View();
        //        }
        //    }
        //    catch(Exception error)
        //    {
        //        // ViewData["message"]=error.Message;
        //        ViewData["message"] = "Echec du bloc try !";
        //        return View();
        //    }

        //}
        //// ViewData["message"] = "Echec de la connexion SQL !";
        //// return View();
        }


        public IActionResult Inscription()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Inscription(UsersModel user)
        {
            // nb : l'extension Microsoft.AspNet.Identity.Owin permet d'avoir un système de connexion deja fait

            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                string email = HttpContext.Request.Form["Email"].ToString();
                string name = HttpContext.Request.Form["Nom"].ToString();
                string userName = HttpContext.Request.Form["Prenom"].ToString();
                string password = HttpContext.Request.Form["Password"].ToString();
                string password2 = HttpContext.Request.Form["Password2"].ToString();
                if (email != "" && name != "" && userName != "" && password != "" && password == password2)
                {
                    // mauvaise facon de hasher un mdp (déconseillé dans la doc asp.net core)
                    //var password = user.Password;
                    //byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
                    //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    //    password: password!,
                    //    salt: salt,
                    //    prf: KeyDerivationPrf.HMACSHA256,
                    //    iterationCount: 100000,
                    //    numBytesRequested: 256 / 8)
                    //);

                    //bonne pratique, utilisation d'identity
                    string PasswordHash(string userName, string password)
                    {
                        PasswordHasher<string> pw = new PasswordHasher<string>();
                        string passwordHashed = pw.HashPassword(userName, password);
                        return passwordHashed;
                    }
                    var PasswordHashed = PasswordHash(userName, password);

                    try
                    {
                        String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=RazorCalendar;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {  
                            connection.Open();
                            String sql = "INSERT INTO dbo.Users (Email, Nom, Prenom, Password) VALUES (@email, @nom, @prenom, @password);";
                            using (SqlCommand command = new SqlCommand(sql, connection))
                            {
                                command.Parameters.AddWithValue("@email", email);
                                command.Parameters.AddWithValue("@nom", name);
                                command.Parameters.AddWithValue("@prenom", userName);
                                command.Parameters.AddWithValue("@password", PasswordHashed);
                                command.ExecuteNonQuery();
                            }
                            connection.Close();
                        }
                        ViewData["message"] = "Inscription effectué !";
                        return View("Connexion");
                    }
                    catch (Exception error)
                    {
                        // Email défini unique dans la bdd
                        // ViewData["message"] = "Cet email est deja utilisé";
                        ViewData["message"] = error.Message;        
                        return View();
                    }
                    
                }
                else
                {
                    // ViewData["message"] = "Echec de l'inscription !";
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