using OpenFlowCRMAPI.Models;
using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpenFlowCRMApp.Controllers
{
    [Authorize(AuthenticationSchemes = "OpenFlowCRMCookie")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdiniAPIController : ControllerBase
    {
        private readonly SQL_TESTContext _context;

        public OrdiniAPIController(SQL_TESTContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet(nameof(Create))]
        public IActionResult Create()
        {
           
           

            try
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
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(Ordini model)
        {
           

            var partiteConfermate = new List<Partite>();

            foreach (var partita in model.Partite)
            {
                partita.Stato = STATO_PARTITA.ORDINE_CONFERMATO;
                partiteConfermate.Add(partita);
            }

            var newOrdine = new Ordini()
            {
                Cliente = model.Cliente,
                Descrizione = model.Descrizione,
                Partite = partiteConfermate
            };

            _context.Add(newOrdine);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
