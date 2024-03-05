using Microsoft.AspNetCore.Mvc;
using Spedizioni.Data;
using Spedizioni.Models;

namespace Spedizioni.Controllers
{
    public class UserController : Controller
    {

        private readonly ApplicationDbContext _db;
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            string hashPassword = PasswordManager.HashPassword(user.Password);
            user.Password = hashPassword;
            if (ModelState.IsValid)
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                TempData["success"] = "Registrazione avvenuta con successo";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["error"] = "Errore nella registrazione";
                return View(user);
            }

        }

    }
}
