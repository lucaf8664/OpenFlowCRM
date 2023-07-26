using OpenFlowCRMModels.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Azure;

namespace OpenFlowCRMApp.Areas.App.Controllers
{
    //[ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private IConfiguration _config;
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _client;

        private readonly ILogger _logger;

        public AuthController(ILogger<AuthController> logger, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("Dev_Client");
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ENDPOINT_URL"));
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        /// <summary>
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {

                var response = await _client.PostAsJsonAsync("api/Users/authenticate", login);
                if (response.IsSuccessStatusCode)
                {
                    var tokenObj = await response.Content.ReadAsStringAsync();

                    // Deserialize the response content to a JSON object
                    var token = JObject.Parse(tokenObj);

                    // Get the token from the response object
                    var tokenString = (string)token["token"];


                    HttpContext.Session.SetString("SessionToken", tokenString);
                    return Ok();
                }
                else
                {
                    _logger.LogInformation($"Error whilen requesting login: {response.Content}", DateTime.UtcNow.ToLongTimeString());
                    return Unauthorized();
                }


            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error while login: {ex}", DateTime.UtcNow.ToLongTimeString());
                throw;
            }
        }


    }
}
