using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OpenFlowCRMApp.Areas.App.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
