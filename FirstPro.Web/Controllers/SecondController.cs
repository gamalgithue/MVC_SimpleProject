using Microsoft.AspNetCore.Mvc;

namespace FirstPro.Web.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
