using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace OpenFlowCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MacchineAPIController : ControllerBase
    {

        private readonly SQL_TESTContext _context;

        public MacchineAPIController(SQL_TESTContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet(nameof(Produzione))]
        public IActionResult Produzione()
        {
            try
            {
                var macchine = _context.Macchine.ToList();


                return Ok(macchine);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet(nameof(GetLottiMacchinaByIDStato))]
        public ActionResult<List<Lotti>> GetLottiMacchinaByIDStato(long id, int stato)
        {
            try
            {
                var lotti = _context.Lotti.Where(l => l.MacchinaAssegnata!=null && l.MacchinaAssegnata == id && l.Stato == (STATO_LOTTO)stato).ToList();

                return Ok(lotti);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet(nameof(GetLottiByMacchinaID))]
        public ActionResult<List<Lotti>> GetLottiByMacchinaID(long id)
        {
            try
            {
                var lotti = _context.Lotti.Where(l => l.MacchinaAssegnata!=null && l.MacchinaAssegnata == id).ToList();

                return Ok(lotti);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet(nameof(GetMacchinaByID))]
        public ActionResult<Macchine> GetMacchinaByID(long id)
        {
            try
            {
                var macchina = _context.Macchine.Where(m => m.MacchineId == id).SingleOrDefault();

                return Ok(macchina);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
