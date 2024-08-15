using AutoMapper;
using FirstPro.BLL.Helper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FirstPro.API.Controllers
{
    public class DepartmentController : BaseController
    {

        #region prop
        private readonly IDepartmentService _department;
        private readonly IMapper mapper;
        #endregion

        #region ctor
        public DepartmentController(IDepartmentService _department, IMapper mapper)
        {
            this._department = _department;
            this.mapper = mapper;
        }

        #endregion

        #region Actions

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> Get()
        {

            try
            {
                var result = await _department.getDepartmentsAsync();
                return Ok(new ApiResponse<IEnumerable<DepartmentDTO>>
                {
                    Code = 200,
                    Status = "Ok",
                    Result = result
                });
            }

            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = 404,
                    Status = "Not Found",
                    Result = ex.Message
                });



            }
        }


        [HttpGet("GetDepartment/{id}")]

        public async Task<IActionResult> Details(int id)
        {

            try
            {
                var result = await _department.getDepartmentAsync(id);
                return Ok(new ApiResponse<DepartmentDTO>
                {
                    Code = 200,
                    Status = "Ok",
                    Result = result
                });

            }
            catch (Exception ex)
            {

                return NotFound(new ApiResponse<string>
                {
                    Code = 404,
                    Status = "Not Found",
                    Result = ex.Message
                });



            }


        }


        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> Create(DepartmentDTO department)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    await _department.CreateOrUpdateDepartmentAsync(department);

                    return Ok(new ApiResponse<string>
                    { 
                        Code = 200,
                        Status = "Ok",
                        Result = "Department Created Successfully"
                    });

                }
                else
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Code = 400,
                        Status = "Bad Request",
                        Result = "Invalid Department Data"
                    });

                }


            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = 404,
                    Status = "Not Found",
                    Result = ex.Message
                });
            }
           
        }

        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> Update(DepartmentDTO department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _department.CreateOrUpdateDepartmentAsync(department);

                    return Ok(new ApiResponse<string>
                    {
                        Code = 200,
                        Status = "Ok",
                        Result = "Department Updated Successfully"
                    });

                }
                else
                {
                    return BadRequest(new ApiResponse<string>
                    {
                        Code = 400,
                        Status = "Bad Request",
                        Result = "Invalid Department Data"
                    });

                }


            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = 404,
                    Status = "Not Found",
                    Result = ex.Message
                });
            }


        }

        [HttpDelete("DeleteDepartment")]

        public async Task<IActionResult> Delete(DepartmentDTO department)
        {
          try
            {
                await _department.DeleteDepartmentAsync(department);

                return Ok(new ApiResponse<string>
                {
                    Code = 200,
                    Status = "Ok",
                    Result = "Department Deleted Successfully"
                });

            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = 404,
                    Status = "Not Found",
                    Result = ex.Message
                });
            }




        }

        #endregion
    }
}



