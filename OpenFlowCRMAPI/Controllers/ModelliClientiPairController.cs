using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpenFlowCRMAPI.Controllers
{
   

    //[Authorize(AuthenticationSchemes = "OpenFlowCRMCookie")]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ModelliClientiPairController : ControllerBase
    {
        private readonly SQL_TESTContext _context;

        public ModelliClientiPairController(SQL_TESTContext context)
        {
            _context = context;
        }
        [HttpGet(nameof(Get))]
        public IActionResult Get()
        {
            var CatalogoModelli = _context.CatalogoModelli.Select(m => $"{m.Id};{m.Descrizione}").ToList();

            var listaClienti = _context.Clienti;

            var listaClientiCombo = listaClienti.Select(f => new SelectListItem { Value = f.Idcliente.ToString(), Text = f.Nome }).ToList();
           

            // serialize CatalogoModelli and listaClientiCombo in json in one object
            var jsonModel = new ModelliClientiPair
            {
                CatalogoModelli = CatalogoModelli,
                Clienti =listaClientiCombo
            };


            return Ok(jsonModel);
        }
    }
}

