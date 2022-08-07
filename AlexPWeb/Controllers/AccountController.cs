using Microsoft.AspNetCore.Mvc;

namespace AlexPWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
