using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Spedizioni.Data;
using Spedizioni.Models;
using System.Security.Claims;


namespace Spedizioni.Controllers
{
    public class LoginController : Controller
    {

        private readonly ApplicationDbContext _db;
        // IAuthenticationSchemeProvider è un servizio che fornisce informazioni sui provider di autenticazione disponibili.
        // Uno schema di autenticazione è un modo per associare un nome a una particolare configurazione di autenticazione.
        // Ci possono essere schemi basati su Cookies o su Bearer Tokens, ad esempio.   
        // GetDefaltAuthenticateSchemeAsync() restituisce lo schema di autenticazione predefinito per l'applicazione quando non viene specificato alcuno schema.
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        // Questo costruttore accetta un parametro di tipo ApplicationBuilder e uno di tipo IAuthenticationSchemeProvider.
        // Il primo parametro è un'istanza di ApplicationBuilder, che è una classe che rappresenta un'istanza di un'applicazione ASP.NET Core.
        // Il secondo parametro è un'istanza di IAuthenticationSchemeProvider, che è un servizio che fornisce informazioni sui provider di autenticazione disponibili.
        // Questo costruttore assegna i valori dei parametri ai campi privati corrispondenti.   

        public LoginController(ApplicationDbContext db, IAuthenticationSchemeProvider schemeProvider)
        {
            _db = db;
            _schemeProvider = schemeProvider;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            // Trova l'utente nel database
            // Questo esegue una query sul db per trovare un utente che ha lo stesso nome dell'utente che sta cercando di effettuare il login.
            // SingleOrDefault è un metodo LINQ che restituisce un singolo elemento da una collezione, o un valore predefinito se la collezione è vuota.
            // In questo caso se non esiste un utente con lo stesso nome utente, dbUser sarà null.
            var dbUser = _db.Users.SingleOrDefault(u => u.Username == user.Username);

            // Se l'utente non esiste, restituisci un errore
            if (dbUser == null)
            {
                //TempData["error"] = "Questo Username non esiste";
                return View();
            }

            // Verifica la password
            if (!PasswordManager.VerifyPassword(user.Password, dbUser.Password))
            {
                //TempData["error"] = "Password non corretta";
                return View();
            }

            // Autentica l'utente
            // I claim rappresentano le informazioni sull'utente autenticato e vengono utilizzati per creare l'identità dell'utente autenticato.
            // Quindi qui creiamo una nuova lista di claim che contiene il nome utente e il ruolo dell'utente.
            //TempData["success"] = "Login effettuato con successo";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbUser.Username),
                new Claim(ClaimTypes.Role, dbUser.Role),
            };

            // ClaimIdentity rappresenta l'identità dell'utente autenticato.
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // SignInAsync crea un cookie di autenticazione per l'utente specificato.
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // Reindirizza l'utente alla pagina principale
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["success"] = "Logout effettuato con successo";
            return RedirectToAction("Index", "Home");
        }

    }
}
