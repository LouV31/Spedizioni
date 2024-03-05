using System.ComponentModel.DataAnnotations;

namespace Spedizioni.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nome Utente")]
        [Required(ErrorMessage = "Il campo 'Nome Utente' non può essere vuoto")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Il campo 'Password' non può essere vuoto")]
        public string Password { get; set; }
        public string Role { get; set; } = "user";
    }
}
