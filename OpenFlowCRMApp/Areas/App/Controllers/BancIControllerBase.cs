using Microsoft.AspNetCore.Mvc;

namespace BancIApp.Areas.App.Controllers
{
    public class BancIControllerBase : Controller
    {

        protected readonly IHttpClientFactory _httpClientFactory;

        protected readonly HttpClient _client;

        protected readonly IConfiguration _config;

        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected string _sessiontoken;

        public BancIControllerBase(IHttpClientFactory httpClientFactory, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _config = config;
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("Dev_Client");
            _client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("ENDPOINT_URL"));
            _sessiontoken = _httpContextAccessor.HttpContext.Session.GetString("SessionToken");
        }

    }
}
