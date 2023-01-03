using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace WebAppLearn.Models
{
    public class UsersModel
    {
        [Required(ErrorMessage = "L'adresse mail est obligatoire")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prenom est obligatoire")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        public string Password { get; set; } = string.Empty;

    }
}
