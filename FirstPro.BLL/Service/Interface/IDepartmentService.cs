using FirstPro.BLL.Modals;
using FirstPro.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Interface
{
   public interface IDepartmentService:IGenericRepository<Department>
    {
        //public Task<List<Department>> GetAsync(Expression<Func<Department,bool>> filter);
        //public Task<Department> GetAsyncById(Expression<Func<Department, bool>> filter);
        //public Task CreateAsync(Department department);
        //public Task UpdateAsync(Department department);
        //public Task DeleteAsync(Department department);


    }
}
