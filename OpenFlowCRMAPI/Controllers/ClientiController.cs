using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenFlowCRMModels.Models;

namespace OpenFlowCRMAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientiController : Controller
    {
        private readonly SQL_TESTContext _context;

        public ClientiController(SQL_TESTContext dbContext)
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
