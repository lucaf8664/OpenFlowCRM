using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenFlowCRMModels;
using Microsoft.AspNetCore.Authorization;
using OpenFlowCRMModels.Models;

namespace OpenFlowCRMAPI.Controllers
{

    //[Authorize(AuthenticationSchemes = "OpenFlowCRMCookie")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogoModelliController : ControllerBase
    {
        private readonly SQL_TESTContext _context;

        public CatalogoModelliController(SQL_TESTContext context)
        {
            _context = context;
        }

        // GET: CatalogoModelli
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.CatalogoModelli != null ? 
                          Ok(await _context.CatalogoModelli.ToListAsync()) :
                          Problem("Entity set 'sql_testContext.CatalogoModelli'  is null.");
        }

        // GET: CatalogoModelli/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.CatalogoModelli == null)
            {
                return NotFound();
            }

            var CatalogoModelli = await _context.CatalogoModelli
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CatalogoModelli == null)
            {
                return NotFound();
            }

            return Ok(CatalogoModelli);
        }


        // POST: CatalogoModelli/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descrizione,Lunghezza,Larghezza,Altezza")] CatalogoModelli CatalogoModelli)
        {
            if (ModelState.IsValid)
            {
                _context.Add(CatalogoModelli);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return Ok(CatalogoModelli);
        }

        // GET: CatalogoModelli/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.CatalogoModelli == null)
            {
                return NotFound();
            }

            var CatalogoModelli = await _context.CatalogoModelli.FindAsync(id);
            if (CatalogoModelli == null)
            {
                return NotFound();
            }
            return Ok(CatalogoModelli);
        }

        // POST: CatalogoModelli/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Descrizione,Lunghezza,Larghezza,Altezza")] CatalogoModelli CatalogoModelli)
        {
            if (id != CatalogoModelli.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(CatalogoModelli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogoModelliExists(CatalogoModelli.Id))
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
            return Ok(CatalogoModelli);
        }

        // DELETE: CatalogoModelli/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.CatalogoModelli == null)
            {
                return NotFound();
            }

            var CatalogoModelli = await _context.CatalogoModelli
                .FirstOrDefaultAsync(m => m.Id == id);
            if (CatalogoModelli == null)
            {
                return NotFound();
            }

            return Ok(CatalogoModelli);
        }

        // POST: CatalogoModelli/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.CatalogoModelli == null)
            {
                return Problem("Entity set 'sql_testContext.CatalogoModelli'  is null.");
            }
            var CatalogoModelli = await _context.CatalogoModelli.FindAsync(id);
            if (CatalogoModelli != null)
            {
                _context.CatalogoModelli.Remove(CatalogoModelli);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogoModelliExists(long id)
        {
          return (_context.CatalogoModelli?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
