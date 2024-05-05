using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.BLL.Service.Repoistory;
using FirstPro.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FirstPro.Web.Controllers
{
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
        public async Task<IActionResult> Index(string? SearchValue)
        {
            if (SearchValue == null)
            {
                var depts = await _department.GetAsync(x => x.Name != null);

                var result = mapper.Map<List<DepartmentDTO>>(depts);


                return View(result);

            }
            else
            {
                var depts = await _department.GetAsync(x => x.Name.Contains(SearchValue)
                || x.Code.Contains(SearchValue) || x.NoOfEmp.ToString() == SearchValue
                || x.Id.ToString() == SearchValue);
                var result = mapper.Map<List<DepartmentDTO>>(depts);


                return View(result);
            }
        }
        
          
        public async Task<IActionResult> Details(int id)
        {
            var depts = await _department.GetAsyncById(x => x.Id==id);
            var result = mapper.Map<DepartmentDTO>(depts);



            return View(result);
        }
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO department)
        {
            var result = mapper.Map<Department>(department);
            await _department.CreateAsync(result);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var depts = await _department.GetAsyncById(x => x.Id == id);
            var result = mapper.Map<DepartmentDTO>(depts);



            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(DepartmentDTO department)
        {
            if (ModelState.IsValid == true)
            {
                var result = mapper.Map<Department>(department);
                await _department.UpdateAsync(result);



                return RedirectToAction("Index");
            }
            else
                return View(department);

           
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {

          
            var depts = await _department.GetAsyncById(x => x.Id == id);
            var result = mapper.Map<DepartmentDTO>(depts);


            return View(result);
        }
        [HttpPost]

        //[ActionName("Delete")]
        public async Task<IActionResult>  Delete(DepartmentDTO department)
        {
          
                var result = mapper.Map<Department>(department);
                await _department.DeleteAsync(result);



                return RedirectToAction("Index");
           

           
        }
        





    }
}
