using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.BLL.Service.Repoistory;
using FirstPro.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstPro.Web.Controllers
{

    [Authorize(Roles = "Admin,HR")]

    public class DepartmentController : Controller
    {

        #region prop
        private readonly IDepartmentService _department;
        private readonly IMapper mapper;
        #endregion

        #region ctor
        public DepartmentController(IDepartmentService _department,IMapper mapper)
        {
            this._department = _department;
            this.mapper = mapper;
        }

        #endregion

        #region Actions
        public async Task<IActionResult> Index()
        {
            var result = await _department.getDepartmentsAsync();
            return View(result);
        }
          
        public async Task<IActionResult> Details(int id)
        {
            var result = await _department.getDepartmentAsync(id);



            return View(result);
        }
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO department)
        {
            await _department.CreateOrUpdateDepartmentAsync(department);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result= await _department.getDepartmentAsync(id);



            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentDTO department)
        {
            if (ModelState.IsValid == true)
            {
                await _department.CreateOrUpdateDepartmentAsync(department);



                return RedirectToAction("Index");
            }
            else
                return View(department);

           
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

          
            var result = await _department.getDepartmentAsync(id);


            return View(result);
        }
        [HttpPost]

        //[ActionName("Delete")]
        public async Task<IActionResult>  Delete(DepartmentDTO department)
        {
          
                await _department.DeleteDepartmentAsync(department);



                return RedirectToAction("Index");
           

           
        }

        #endregion




    }
}
