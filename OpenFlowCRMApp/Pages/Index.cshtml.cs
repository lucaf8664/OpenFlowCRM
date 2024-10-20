using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using OpenFlowCRMModels.DTOs;

namespace OpenFlowCRMApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        [BindProperty]
        public LoginDTO Utente { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory=httpClientFactory;
        }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync() 
        {

            var httpClient = _httpClientFactory.CreateClient("CRM_API");
            var httpResponseMessage = await httpClient.PostAsJsonAsync("api/Utenti/authenticate", Utente);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                // show home page
                var tokenString = await httpResponseMessage.Content.ReadAsStringAsync();

                HttpContext.Session.SetString("SessionToken", tokenString);
                
                return RedirectToPage("/Home");
            }

            return Page();

        }


    }
}
