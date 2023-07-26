using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OpenFlowCRMAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenFlowCRMCookie")]
    [Route("api/[controller]")]
    [ApiController]
    public class PartiteAPIController : ControllerBase
    {
        private readonly SQL_TESTContext _context;

        public PartiteAPIController(SQL_TESTContext context)
        {
            _context = context;
        }

        // GET: api/Partite
        [HttpGet(nameof(GetPartite))]
        public async Task<ActionResult<IEnumerable<Partite>>> GetPartite()
        {
            try
            {
                if (_context.Partite == null)
                {
                    return NotFound();
                }

                var partite = await _context.Partite.ToListAsync();

                return Ok(partite);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet(nameof(PartiteCaricate))]
        // GET: Partite
        public async Task<IActionResult> PartiteCaricate()
        {
            var sql_testContext = _context.Partite.Include(p => p.ModelloNavigation).Include(p => p.OrdineNavigation);

            var partiteCaricate = sql_testContext.Where(p => p.Stato == STATO_PARTITA.CARICATA);

            return Ok(await partiteCaricate.ToListAsync());
        }

        // GET: Partite
        [HttpGet(nameof(PianoDiCarico))]
        public async Task<IActionResult> PianoDiCarico()
        {
            var sql_testContext = _context.Partite.Include(p => p.ModelloNavigation).Include(p => p.OrdineNavigation);

            var partiteConfermate = sql_testContext.Where(p => p.Stato == STATO_PARTITA.ORDINE_CONFERMATO);

            return Ok(await partiteConfermate.ToListAsync());
        }

        // POST: Partite
        [HttpGet(nameof(AvvenutoCarico))]
        public async Task<IActionResult> AvvenutoCarico(long id)
        {
            await SetPartitaStatus(id, STATO_PARTITA.CARICATA);

            var sql_testContext = _context.Partite.Include(p => p.ModelloNavigation).Include(p => p.OrdineNavigation);

            var partiteConfermate = sql_testContext.Where(p => p.Stato == STATO_PARTITA.ORDINE_CONFERMATO);

            return Ok(await partiteConfermate.ToListAsync());
        }

        private async Task<StatusCodeResult> SetPartitaStatus(long id, STATO_PARTITA cARICATA)
        {

            try
            {
                var partita = await _context.Partite.FindAsync(id);
                if (partita == null)
                {
                    return NotFound();
                }

                partita.Stato=cARICATA;

                _context.Update(partita);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }
            return Ok();
        }

        // GET: api/Partite/5
        [HttpGet(nameof(GetPartiteById))]
        public async Task<ActionResult<Partite>> GetPartiteById(long id)
        {
            if (_context.Partite == null)
            {
                return NotFound();
            }
            var partite = await _context.Partite.FindAsync(id);

            if (partite == null)
            {
                return NotFound();
            }

            return partite;
        }

        // PUT: api/Partite/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPartite(long id, Partite partite)
        {
            if (id != partite.PartiteId)
            {
                return BadRequest();
            }

            _context.Entry(partite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartiteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Partite
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Partite>> PostPartite(Partite partite)
        {
            if (_context.Partite == null)
            {
                return Problem("Entity set 'SQL_TESTContext.Partite'  is null.");
            }
            _context.Partite.Add(partite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPartite", new { id = partite.PartiteId }, partite);
        }

        // DELETE: api/Partite/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartite(long id)
        {
            if (_context.Partite == null)
            {
                return NotFound();
            }
            var partite = await _context.Partite.FindAsync(id);
            if (partite == null)
            {
                return NotFound();
            }

            _context.Partite.Remove(partite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PartiteExists(long id)
        {
            return (_context.Partite?.Any(e => e.PartiteId == id)).GetValueOrDefault();
        }
    }
}
