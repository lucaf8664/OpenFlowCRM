using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;

namespace OpenFlowCRMApp.Areas.App.Controllers
{

    public class PartiteController : Controller
    {
        private IConfiguration _config;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public PartiteController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("Dev_Client");
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ENDPOINT_URL"));

        }

        // GET: Partite
        public async Task<IActionResult> Index()
        {
            try
            {
                // Make the REST API call
                var response = await _client.GetAsync("api/PartiteAPI/GetPartite");

                // Ensure that the response is successful
                response.EnsureSuccessStatusCode();




                // Read the response content as a string
                // deserialize json into list of Partite
                List<Partite> partiteList = JsonConvert.DeserializeObject<List<Partite>>(await response.Content.ReadAsStringAsync());

                return View(partiteList);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // GET: Partite
        [HttpGet]
        public async Task<IActionResult> PartiteCaricate()
        {


            var res = await _client.GetAsync("api/PartiteAPI/PartiteCaricate");
            var partiteCaricate = JsonConvert.DeserializeObject<List<Partite>>(await res.Content.ReadAsStringAsync());

            return View(nameof(PianoDiCarico), partiteCaricate);
        }

        [HttpGet]
        // GET: Partite
        public async Task<IActionResult> PianoDiCarico()
        {
            var res = await _client.GetAsync("api/PartiteAPI/PianoDiCarico");

            res.EnsureSuccessStatusCode();

            var pianoDiCarico = JsonConvert.DeserializeObject<List<Partite>>(await res.Content.ReadAsStringAsync());

            return View(nameof(PianoDiCarico), pianoDiCarico);
        }

        // GET: Partite
        [HttpGet]
        public async Task<IActionResult> AvvenutoCarico(long id)
        {



            var res = await _client.GetAsync($"api/PartiteAPI/AvvenutoCarico?id={id}");
            var partiteConfermate = JsonConvert.DeserializeObject<List<Partite>>(await res.Content.ReadAsStringAsync());

            return View(nameof(PianoDiCarico), partiteConfermate);
        }

        private async Task<StatusCodeResult> SetPartitaStatus(long id, STATO_PARTITA cARICATA)
        {

            try
            {
                var response = await _client.GetAsync($"api/SetPartitaStatus?id={id}&cARICATA={cARICATA}");
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        // GET: Partite/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            var res = await _client.GetAsync($"api/Partite?id={id}");
            var partita = JsonConvert.DeserializeObject<Partite>(await res.Content.ReadAsStringAsync());

            return View(nameof(PianoDiCarico), partita);

        }


        // GET: Partite/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partite = await _client.GetFromJsonAsync<Partite>($"api/PartiteAPI/GetPartiteById?id={id}");
            if (partite == null)
            {
                return NotFound();
            }

            var mcPair = await _client.GetFromJsonAsync<ModelliClientiPair>("api/ModelliClientiPair/Get");

            var modelliCombo = mcPair.CatalogoModelli.Select(m => new SelectListItem(value: m.Split(';')[0], text: m.Split(';')[1]));

            ViewBag.Modello = modelliCombo;


            return View(partite);
        }

        // POST: Partite/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PartiteId,Descrizione,Modello,PezziAlCarico,Ncarichi,Richiesta,DataConsegna,Ordine")] Partite partite)
        {
            if (id != partite.PartiteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //PUT partite
                    var res = await _client.PutAsJsonAsync($"api/PartiteAPI/{id}", partite);
                }
                catch (Exception)
                {
                    throw;

                }
                return RedirectToAction(nameof(Index));
            }

            var response = await _client.GetFromJsonAsync<ModelliClientiPair>("api/ModelliClientiPair/Get");


            ViewData["CatalogoModelli"] = response.CatalogoModelli;

            ViewData["ListaClienti"] = response.Clienti;

            return View(partite);
        }

        // GET: Partite/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partite = await _client.GetFromJsonAsync<Partite>($"api/PartiteAPI/GetPartiteById?id={id}");
            if (partite == null)
            {
                return NotFound();
            }

            return View(partite);
        }

        // POST: Partite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {

            var partite = await _client.GetAsync($"api/PartiteAPI/GetPartiteById?id={id}");
            if (partite != null)
            {
                await _client.DeleteAsync($"api/PartiteAPI/{id}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
