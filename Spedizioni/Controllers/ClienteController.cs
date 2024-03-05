using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spedizioni.Data;
using Spedizioni.Models;

namespace Spedizioni.Controllers
{

    [Authorize(Roles = "admin")]
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ClienteController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Cliente> objList = _db.Clienti;
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente obj)
        {
            if (ModelState.IsValid && (obj.TipoCliente.ToLower() == "azienda" || obj.TipoCliente.ToLower() == "privato"))


            {


                _db.Clienti.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");



            }
            else
            {
                return View(obj);

            }
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return View("Error");
            }
            var obj = _db.Clienti.Find(id);
            if (obj == null)
            {
                return View("Error");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, Cliente obj)
        {
            if (id != obj.Id)
            {
                return View("Error");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(obj);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExist(obj.Id))
                    {
                        return View("Error");
                    }

                }
                return RedirectToAction("Index");
            }


            return View(obj);
        }
        private bool ClienteExist(int id)
        {
            return _db.Clienti.Any(e => e.Id == id);
        }
    }
}
