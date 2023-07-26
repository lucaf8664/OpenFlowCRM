using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OpenFlowCRMAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenFlowCRMCookie")]
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentiMerceAPIController : ControllerBase
    {
        private readonly SQL_TESTContext _context;

        public ComponentiMerceAPIController(SQL_TESTContext context)
        {
            _context = context;
        }

        [HttpGet(nameof(PianoRiordino))]
        public IActionResult PianoRiordino()
        {
            var PianoRiordino = _context.VwPianoRiordino.ToList();

            return Ok(PianoRiordino.ToList());
        }

        // GET: api/ComponentiMerceAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ComponentiMerce>>> GetComponentiMerce()
        {
            if (_context.ComponentiMerce == null)
            {
                return NotFound();
            }
            return await _context.ComponentiMerce.ToListAsync();
        }

        // GET: api/ComponentiMerceAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentiMerce>> GetComponentiMerce(long id)
        {
            if (_context.ComponentiMerce == null)
            {
                return NotFound();
            }
            var componentiMerce = await _context.ComponentiMerce.FindAsync(id);

            if (componentiMerce == null)
            {
                return NotFound();
            }

            return componentiMerce;
        }

        // PUT: api/ComponentiMerceAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComponentiMerce(long id, ComponentiMerce componentiMerce)
        {
            if (id != componentiMerce.IdcomponenteMerce)
            {
                return BadRequest();
            }

            _context.Entry(componentiMerce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentiMerceExists(id))
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

        // POST: api/ComponentiMerceAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ComponentiMerce>> PostComponentiMerce(ComponentiMerce componentiMerce)
        {
            if (_context.ComponentiMerce == null)
            {
                return Problem("Entity set 'SQL_TESTContext.ComponentiMerce'  is null.");
            }
            _context.ComponentiMerce.Add(componentiMerce);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComponentiMerce", new { id = componentiMerce.IdcomponenteMerce }, componentiMerce);
        }

        // DELETE: api/ComponentiMerceAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComponentiMerce(long id)
        {
            if (_context.ComponentiMerce == null)
            {
                return NotFound();
            }
            var componentiMerce = await _context.ComponentiMerce.FindAsync(id);
            if (componentiMerce == null)
            {
                return NotFound();
            }

            _context.ComponentiMerce.Remove(componentiMerce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComponentiMerceExists(long id)
        {
            return (_context.ComponentiMerce?.Any(e => e.IdcomponenteMerce == id)).GetValueOrDefault();
        }
    }
}
