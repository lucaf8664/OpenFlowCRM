﻿using Microsoft.AspNetCore.Mvc;
using OpenFlowCRMModels.Models;

namespace OpenFlowCRMApp.Areas.App.Controllers
{
    public class ProduzioneController : Controller
    {

        private IConfiguration _config;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        public ProduzioneController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("CRM_API");

        }

        public async Task<IActionResult> Index()
        {

            var macchine = await _client.GetFromJsonAsync<List<Macchine>>("api/MacchineAPI/Produzione");


            return View(macchine);
        }

        public async Task<IActionResult> PianificazioneLotti()
        {

            var lotti = await _client.GetFromJsonAsync<List<Lotti>>("api/LottiAPI/LottiNonAssegnati");


            return View(lotti);
        }


    }
}
