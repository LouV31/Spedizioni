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
        // Memorizzo l'istanza di ApplicationDbContext in _db
        // è privata perché così è accessibile solo all'interno di questa classe
        // readonly significa che non posso modificarla
        private readonly ApplicationDbContext _db;

        // Innietto un istanza del db nel costruttore
        public SpedizioneController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Spedizione
        public async Task<IActionResult> Index()
        {
            // Inizia una query sul db, seleziona tutte le spedizioni.
            // Include(s => s.Cliente) dice a Entity Framework di includere i dati del cliente
            var applicationDbContext = _db.Spedizione.Include(s => s.Cliente);
            // Esegue la query e restituisce una Lista di spedizioni alla View 
            return View(await applicationDbContext.ToListAsync());
        }

        // GET
        public async Task<IActionResult> Details(int? id)
        {
            // Se l'id è nullo, restituisce NotFound
            if (id == null)
            {
                return NotFound();
            }

            // Esegue una query sul db per trovare la spedizione con l'id specificato includendo anche i dati del cliente associato alla spedizione
            // FirstOrDefaultAsync restituisce il primo elemento che corrisponde alla query o null se non trova nulla
            var spedizione = await _db.Spedizione
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.IdSpedizione == id);
            // Nel caso in cui non trovi nulla, restituisce NotFound
            if (spedizione == null)
            {
                return NotFound();
            }

            // Altrimenti restituisce la spedizione alla View
            return View(spedizione);
        }

        // GET 
        public IActionResult Create()
        {
            // Crea una nuova lista di selezione con gli Id dei clienti come valori e i nomi dei clienti come testo
            // ViewData è un dizionario che contiene i dati che verranno passati alla View
            // Un dizionario è una struttura dati che contiene coppie chiave-valore
            ViewData["IdCliente"] = new SelectList(_db.Clienti, "Id", "Nome");
            return View();
        }

        // POST
        [HttpPost]
        // ValidateAntiForgeryToken è un attributo che protegge da attacchi CSRF
        // In che modo? Quando un utente invia un form, viene generato un token che viene inviato al server
        // Se il token non corrisponde a quello generato dal server, il server rifiuta la richiesta
        [ValidateAntiForgeryToken]
        // Bind è un attributo che specifica quali proprietà del modello vengono incluse nell'operazione di associazione dei modelli
        public async Task<IActionResult> Create([Bind("IdSpedizione,IdCliente,DataSpedizione,peso,Destinazione,NominativoDestinatario,Indirizzo,PrezzoSpedizione,DataConsegna")] Spedizione spedizione)
        {
            try
            {
                // Nel try si prova a salvare la nuova spedizione nel db
                // Se non ci sono errori, si salva e si ritorna alla pagina Index
                _db.Spedizione.Add(spedizione);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));


            }
            catch (DbUpdateException)
            {
                // Se c'è un errore, si aggiunge un errore al modello.
                ModelState.AddModelError("", "Impossibile salvare i dati.");
            }

            ViewData["IdCliente"] = new SelectList(_db.Clienti, "Id", "Nome", spedizione.IdCliente);
            return View(spedizione);
        }

        // GET
        public async Task<IActionResult> Edit(int? id)
        {
            // Controllo se l'id è nullo, in caso affermativo restituisce NotFound
            if (id == null)
            {
                return NotFound();
            }

            // Eseguo una query sul db per trovare la spedizione con l'id specificato   
            // Include(s => s.Cliente) dice a Entity Framework di includere i dati del cliente associato alla spedizione
            var spedizione = await _db.Spedizione.FindAsync(id);
            // Gestisco il caso in cui sia null
            if (spedizione == null)
            {
                return NotFound();
            }

            // Nel caso in cui la spedizione non sia nulla, restituisco la spedizione alla View
            return View(spedizione);
        }

        // POST
        [HttpPost]
        // Protegge da attacchi CSRF associando un token alla richiesta e se il token non corrisponde a quello generato dal server, la richiesta viene rifiutata    
        [ValidateAntiForgeryToken]
        // Uso l'attributo Bind per specificare quali proprietà del modello vengono incluse nell'operazione di associazione dei modelli
        public async Task<IActionResult> Edit(int id, [Bind("IdSpedizione,IdCliente,DataSpedizione,peso,Destinazione,NominativoDestinatario,Indirizzo,PrezzoSpedizione,DataConsegna")] Spedizione spedizione)
        {
            // Se l'id che passo non corrisponde all'id della spedizione, restituisce NotFound
            if (id != spedizione.IdSpedizione)
            {
                return NotFound();
            }




            ModelState.Remove("Cliente");
            if (ModelState.IsValid)
            {

                try
                {
                    // Update è un metodo di Entity Framework che aggiorna l'entità nel db
                    _db.Spedizione.Update(spedizione);
                    // SaveChangesAsync salva le modifiche nel db
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
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

            }
            else
            {
                // Log degli errori di ModelState
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                    .ToList();
                // Registrare nel log di sistema o in un file
                foreach (var entry in errors)
                {
                    foreach (var error in entry.Errors)
                    {
                        // Utilizza il tuo sistema di logging preferito qui
                        System.Diagnostics.Debug.WriteLine($"Errore nel campo {entry.Key}: {error}");
                    }
                }


            }
            return View(spedizione);
        }

        // GET
        public async Task<IActionResult> Delete(int? id)
        {
            // Controlla se l'id che passo non è null
            if (id == null)
            {
                return NotFound();
            }

            // Eseguo una query sul db per trovare la spedizione con l'id specificato e includo i dati del cliente associato
            // FirstOrDefaultAsync restituisce il primo elemento che corrisponde alla query o null se non trova nulla
            var spedizione = await _db.Spedizione
                .Include(s => s.Cliente)
                .FirstOrDefaultAsync(m => m.IdSpedizione == id);
            // Se non trova nulla, restituisce NotFound 
            if (spedizione == null)
            {
                return NotFound();
            }

            // Passo la spedizione alla View
            return View(spedizione);
        }

        // POST
        // ActionName serve per specificare il nome dell'azione
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Eseguo una query sul db per trovare la spedizione con l'id specificato
            // FindAsync mi restituisce la spedizione con l'id specificato o null se non trova nulla
            var spedizione = await _db.Spedizione.FindAsync(id);
            if (spedizione != null)
            {
                // Se trova la spedizione la rimuove con Remove.
                _db.Spedizione.Remove(spedizione);
            }

            // e salva le modifiche nel db
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Metodo che controlla se una spedizione esiste
        private bool SpedizioneExists(int id)
        {
            // Any è un metodo di LINQ che restituisce true se almeno un elemento della sequenza soddisfa la condizione specificata
            return _db.Spedizione.Any(e => e.IdSpedizione == id);
        }
    }
}
