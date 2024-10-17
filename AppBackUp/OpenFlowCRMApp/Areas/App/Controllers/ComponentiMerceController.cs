using OpenFlowCRMModels.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OpenFlowCRMApp.Areas.App.Controllers
{
    public class ComponentiMerceController : Controller
    {
        private IConfiguration _config;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public ComponentiMerceController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("Dev_Client");
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ENDPOINT_URL"));
        }

        public IActionResult PianoRiordino()
        {

            // call the API
            var response = _client.GetAsync("api/ComponentiMerceAPI/PianoRiordino").Result;
            // check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // read the response content
                var content = response.Content.ReadAsStringAsync().Result;
                // deserialize the content
                var PianoRiordino = JsonConvert.DeserializeObject<List<VwPianoRiordino>>(content);
                // return the view with the model
                return View(PianoRiordino);
            }
            else
            {
                // return the error view
                return View("Error");
            }
        }


    }
}
