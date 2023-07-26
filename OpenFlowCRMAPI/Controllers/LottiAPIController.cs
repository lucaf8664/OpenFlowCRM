using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace OpenFlowCRMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LottiAPIController : ControllerBase
    {

        private readonly SQL_TESTContext _context;

        public LottiAPIController(SQL_TESTContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet(nameof(LottiNonAssegnati))]
        public ActionResult<List<Lotti>> LottiNonAssegnati()
        {
            var lotti = _context.Lotti.Where(l => l.MacchinaAssegnata==null || l.MacchinaAssegnata < 0).ToList();

            return Ok(lotti);
        }
    }
}
