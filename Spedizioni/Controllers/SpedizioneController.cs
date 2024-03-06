using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spedizioni.Data;
using Spedizioni.Models;

namespace Spedizioni.Controllers
{
    [Authorize(Roles = "admin")]
    public class SpedizioneController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpedizioneController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Spedizione
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Spedizione.Include(s => s.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Spedizione/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spedizione = await _context.Spedizione
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.IdSpedizione == id);
            if (spedizione == null)
            {
                return NotFound();
            }

            return View(spedizione);
        }

        // GET: Spedizione/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "Id", "Nome");
            return View();
        }

        // POST: Spedizione/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSpedizione,IdCliente,DataSpedizione,peso,Destinazione,NominativoDestinatario,Indirizzo,PrezzoSpedizione,DataConsegna")] Spedizione spedizione)
        {
            try
            {
                _context.Add(spedizione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            catch (DbUpdateException)
            {
                // scrivimi l'errore
                ModelState.AddModelError("", "Impossibile salvare i dati.");
            }

            ViewData["IdCliente"] = new SelectList(_context.Clienti, "Id", "Nome", spedizione.IdCliente);
            return View(spedizione);
        }

        // GET: Spedizione/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spedizione = await _context.Spedizione.FindAsync(id);
            if (spedizione == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "Id", "Nome", spedizione.IdCliente);
            return View(spedizione);
        }

        // POST: Spedizione/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSpedizione,IdCliente,DataSpedizione,peso,Destinazione,NominativoDestinatario,Indirizzo,PrezzoSpedizione,DataConsegna")] Spedizione spedizione)
        {
            if (id != spedizione.IdSpedizione)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spedizione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpedizioneExists(spedizione.IdSpedizione))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clienti, "Id", "Nome", spedizione.IdCliente);
            return View(spedizione);
        }

        // GET: Spedizione/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spedizione = await _context.Spedizione
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.IdSpedizione == id);
            if (spedizione == null)
            {
                return NotFound();
            }

            return View(spedizione);
        }

        // POST: Spedizione/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spedizione = await _context.Spedizione.FindAsync(id);
            if (spedizione != null)
            {
                _context.Spedizione.Remove(spedizione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpedizioneExists(int id)
        {
            return _context.Spedizione.Any(e => e.IdSpedizione == id);
        }
    }
}
