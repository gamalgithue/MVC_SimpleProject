using AutoMapper;
using FirstPro.BLL.Modals;
using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Database;
using FirstPro.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Repoistory
{
    public class DepartmentServic:IDepartmentService
    {


        #region prop
        private readonly IGenericRepository<Department> _departmentservice;
        private readonly IMapper _mapper;
        #endregion

        #region ctor
        public DepartmentServic(IGenericRepository<Department> _departmentservice,IMapper _mapper)
        {
            this._departmentservice = _departmentservice;
            this._mapper = _mapper;
        }
        #endregion

        #region actions
        public async Task<IEnumerable<DepartmentDTO>> getDepartmentsAsync()
        {
            var result = await _departmentservice.GetAsync(null,null,100,false);
            return _mapper.Map<IEnumerable<DepartmentDTO>>(result);
        }
        public async Task<IEnumerable<DepartmentDTO>> getDepartmentsAsync(int page, int pagesize)
        {
            var result = await _departmentservice.GetAsync(null, page, pagesize, false);
            return _mapper.Map<IEnumerable<DepartmentDTO>>(result);
        }

        public async Task<DepartmentDTO> getDepartmentAsync(int id)
        {
            var result = await _departmentservice.GetFirstOrDefaultAsync(x => x.Id == id, false);
            return _mapper.Map<DepartmentDTO>(result);
        }
        public async Task CreateOrUpdateDepartmentAsync(DepartmentDTO department)
        {
            var obj = _mapper.Map<Department>(department);
            await _departmentservice.CreateOrUpdateAsync(obj);

        }



        public async Task DeleteDepartmentAsync(DepartmentDTO department)
        {
            var obj = _mapper.Map<Department>(department);
            await _departmentservice.DeleteAsync(obj);
        }


        #endregion

    }
}
