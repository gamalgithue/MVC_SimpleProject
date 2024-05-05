using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstPro.Web.Controllers
{
    public class EmployeeController : Controller
    {

        #region prop
        private readonly IEmployeeService employee;
        private readonly IMapper mapper;
        private readonly IDepartmentService department;
        #endregion


        #region ctor
        public EmployeeController(IEmployeeService _employee,IMapper _mapper,IDepartmentService department)
        {
            this.employee = _employee;
            this.mapper = _mapper;
            this.department = department;
        }
        #endregion
        public async Task<IActionResult> Index(string? SearchValue)
        {
            if (SearchValue == null)
            {


               var depts = await employee.GetAsync(x=>x.Department);
               depts= await employee.GetAsync(x => x.Name != null);


                var result = mapper.Map<List<EmployeeDTO>>(depts);


                return View(result);

            }
            else
            {


                var depts = await employee.GetAsync(x => x.Department);
                 depts = await employee.GetAsync(x =>
                 x.Name.Contains(SearchValue) ||
                 x.Salary.ToString() == SearchValue ||
              x.Email.Contains(SearchValue) ||
                  x.HireDate.ToString() == SearchValue ||
                x.Department.Name.Contains(SearchValue)
                   );
                var result = mapper.Map<List<EmployeeDTO>>(depts);

                


                return View(result);
            }


        }
        public async Task<IActionResult> Details(int id)
        {

            var depts = await employee.GetAsyncById(x => x.Id == id,x=>x.Department);
            
            var result = mapper.Map<EmployeeDTO>(depts);



            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO employeevm)
        {
            var result = mapper.Map<Employee>(employeevm);
            await employee.CreateAsync(result);
          

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var depts = await employee.GetAsyncById(x => x.Id == id);
            var result = mapper.Map<EmployeeDTO>(depts);
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name",result.DepartmentId);



            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeDTO employeevm)
        {


            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", employeevm.DepartmentId);

            if (ModelState.IsValid == true)
            {
                var result = mapper.Map<Employee>(employeevm);
                await employee.UpdateAsync(result);



                return RedirectToAction("Index");
            }
            else
                return View(employeevm);
        }


            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {


                var depts = await employee.GetAsyncById(x => x.Id == id);
                var result = mapper.Map<EmployeeDTO>(depts);
            ViewBag.DepartmentList = new SelectList(await department.GetAsync(), "Id", "Name", result.DepartmentId);






            return View(result);
            }
            [HttpPost]

            //[ActionName("Delete")]
            public async Task<IActionResult> Delete(EmployeeDTO employeevm)
            {

                var result = mapper.Map<Employee>(employeevm);
                await employee.DeleteAsync(result);



                return RedirectToAction("Index");



            }
        }



    }

