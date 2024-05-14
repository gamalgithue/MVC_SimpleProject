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
        public async Task<IActionResult> Index()
        {
            var emps = await employee.getEmployeesAsync(1, 10);
            return View(emps);

        }
        public async Task<IActionResult> Details(int id)
        {

            var result = await employee.getEmployeeAsync(id);

                return View(result);
        }
        [HttpGet]
        public async Task<IActionResult>  Create()
        {
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO employeevm)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    await employee.CreateOrUpdateEmployeeAsync(employeevm);
                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {

            }
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name");



            return View(employeevm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            
            var emp = await employee.getEmployeeAsync(id);
            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name",emp.DepartmentId);
            


            return View(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Update(EmployeeDTO employeevm)
        {


            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name", employeevm.DepartmentId);

            try
            {
                if (ModelState.IsValid)
                {

                    await employee.CreateOrUpdateEmployeeAsync(employeevm);



                    return RedirectToAction("Index");
                }

            }
           catch(Exception ex)
            {

            }

            return View(employeevm);
        }


            [HttpGet]
            public async Task<IActionResult> Delete(int id)
            {


            var result = await employee.getEmployeeAsync(id);

            ViewBag.DepartmentList = new SelectList(await department.getDepartmentsAsync(), "Id", "Name", result.DepartmentId);






            return View(result);
            }
            [HttpPost]

            //[ActionName("Delete")]
            public async Task<IActionResult> Delete(EmployeeDTO employeevm)
            {

                await employee.DeleteEmployeeAsync(employeevm);

            return RedirectToAction("Index");





        }
        }



    }

