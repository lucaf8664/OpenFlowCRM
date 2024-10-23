using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace OpenFlowCRMApp.Areas.App.Controllers
{


    public class OrdiniController : Controller
    {


        private IConfiguration _config;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public OrdiniController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("CRM_API");
           
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var response = await _client.GetFromJsonAsync<ModelliClientiPair>("api/ModelliClientiPair/Get");


            ViewData["CatalogoModelli"] = response.CatalogoModelli;

            ViewData["ListaClienti"] = response.Clienti;

            var model = new Ordini();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ordini model)
        {
            try
            {

                if(!ModelState.IsValid)
                {
                    return View();
                }

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

                //serialize newOrdine in json
                var serializedObj = Newtonsoft.Json.JsonConvert.SerializeObject(newOrdine);

                JsonContent content = JsonContent.Create(newOrdine);

                var response = await _client.PostAsync("api/OrdiniAPI/Create", content);


                response.EnsureSuccessStatusCode();

                return RedirectToAction();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
