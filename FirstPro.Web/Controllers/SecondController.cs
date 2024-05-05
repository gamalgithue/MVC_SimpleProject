using Microsoft.AspNetCore.Mvc;

namespace FirstPro.Web.Controllers
{
    public class SecondController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Create2()
        {
            return View();
        }
    }
}
