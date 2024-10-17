using BancIModels;
using Microsoft.AspNetCore.Mvc;

namespace BancIApp.Areas.App.Controllers
{
    public class ClientiController : Controller
    {
        private readonly SQL_FRANCESCONContext _context;

        public ClientiController(SQL_FRANCESCONContext context)
        {
            _context = context;
        }

        // GET: Clienti
        public async Task<IActionResult> Index()
        {
            return _context.Clienti != null ?
                        View(await _context.Clienti.ToListAsync()) :
                        Problem("Entity set 'SQL_FRANCESCONContext.Clienti'  is null.");
        }

        // GET: Clienti/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Clienti == null)
            {
                return NotFound();
            }

            var clienti = await _context.Clienti
                .FirstOrDefaultAsync(m => m.Idcliente == id);
            if (clienti == null)
            {
                return NotFound();
            }

            return View(clienti);
        }

        // GET: Clienti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clienti/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idcliente,Nome,Indirizzo,Note")] Clienti clienti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clienti);
        }

        // GET: Clienti/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Clienti == null)
            {
                return NotFound();
            }

            var clienti = await _context.Clienti.FindAsync(id);
            if (clienti == null)
            {
                return NotFound();
            }
            return View(clienti);
        }

        // POST: Clienti/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Idcliente,Nome,Indirizzo,Note")] Clienti clienti)
        {
            if (id != clienti.Idcliente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientiExists(clienti.Idcliente))
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
            return View(clienti);
        }

        // GET: Clienti/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Clienti == null)
            {
                return NotFound();
            }

            var clienti = await _context.Clienti
                .FirstOrDefaultAsync(m => m.Idcliente == id);
            if (clienti == null)
            {
                return NotFound();
            }

            return View(clienti);
        }

        // POST: Clienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Clienti == null)
            {
                return Problem("Entity set 'SQL_FRANCESCONContext.Clienti'  is null.");
            }
            var clienti = await _context.Clienti.FindAsync(id);
            if (clienti != null)
            {
                _context.Clienti.Remove(clienti);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientiExists(long id)
        {
            return (_context.Clienti?.Any(e => e.Idcliente == id)).GetValueOrDefault();
        }
    }
}
