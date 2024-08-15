using FirstPro.BLL.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstPro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {


       // [HttpGet("GetAllData")]
       //public IActionResult GetData()
       // {
       //     string[] names = { "Ali", "Ahmed", "Mohamed", "Zaki" };

       //     if (names.Length > 0)
       //     {
       //         return Ok(new ApiResponse<string[]> { Code = 200, Status = "Ok", Result = names });

       //     }
       //     else
       //     {
       //         return NotFound(new ApiResponse<string> 
       //         { Code = 404, Status = "Not Found", Result = null });
       //     }

       // }
    }
}
