using OpenFlowCRMModels;
using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace OpenFlowCRMApp.Areas.App.Controllers
{
    public class MacchineController : Controller
    {
        private IConfiguration _config;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public MacchineController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("Dev_Client");
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ENDPOINT_URL"));
           
        }

        public async Task<IActionResult> Index(long id)
        {

           

            var macchine = await _client.GetFromJsonAsync<Macchine>($"api/MacchineAPI/GetMacchinaByID?id={id}");


            return View("Index", macchine);
        }

        public async Task<IActionResult> GetLottiMacchinaByID(long id)
        {

            var lotti= await _client.GetFromJsonAsync<List<Lotti>>($"api/MacchineAPI/GetLottiByMacchinaID?id={id}");

            return View("LottiMacchina", lotti);

        }

        public async Task<IActionResult> GetLottiMacchinaByIDStato(long id, int stato)
        {
            try
            {
                var lotti = await _client.GetFromJsonAsync<List<Lotti>>($"api/MacchineAPI/GetLottiMacchinaByIDStato?id={id}&stato={stato}");

               

                return View("LottiMacchina", lotti);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
