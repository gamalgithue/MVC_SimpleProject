using Microsoft.AspNetCore.Mvc;

namespace FirstPro.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
    }
}
