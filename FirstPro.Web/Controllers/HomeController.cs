using FirstPro.Web.Languages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FirstPro.Web.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResource> stringlocalizer;

        public HomeController(IStringLocalizer<SharedResource> _stringlocalizer)
        {
            this.stringlocalizer = _stringlocalizer;
        }
        public IActionResult Index()
        {
            ViewBag.msg = stringlocalizer["Dashboard"];
            return View();
        }
       
    }
}
