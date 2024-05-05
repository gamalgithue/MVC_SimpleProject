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
    public class DepartmentServic: GenericRepository<Department>,IDepartmentService
    {


        public DepartmentServic(ApplicationDbContext ops):base(ops)
        {
            
        }


        //public DepartmentServic(ApplicationDbContext db):base(db)
        //{

        //}
        //private readonly ApplicationDbContext _db;

        //public DepartmentService(ApplicationDbContext _db)
        //{
        //    this._db = _db;
        //}

        //public  async Task<List<Department>> GetAsync(Expression<Func<Department, bool>> filter)
        //{
        //    var result = await _db.Departments.Where(filter).ToListAsync();
        //    return result;
        //}

        //public  async Task<Department> GetAsyncById(Expression<Func<Department, bool>> filter)
        //{
        //    var result = await _db.Departments.Where(filter).FirstOrDefaultAsync();
        //    return result;

        //}
        //public async Task CreateAsync(Department department)
        //{

        //   await _db.Departments.AddAsync(department);
        //   await _db.SaveChangesAsync();


        //}




        //public async Task UpdateAsync(Department department)
        //{
        //    var olddata =  await _db.Departments.FindAsync(department.Id);

        //    olddata.Name = department.Name;
        //    olddata.Code = department.Code;
        //    olddata.NoOfEmp = department.NoOfEmp;
        //    await _db.SaveChangesAsync();

        //}
        //public async Task DeleteAsync(Department department)
        //{
        //    //var olddata = await  _db.Departments.FindAsync(department.Id);



        //    _db.Departments.Remove(department);
        //    await _db.SaveChangesAsync();

        //}

    }
}
