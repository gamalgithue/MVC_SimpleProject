using FirstPro.BLL.Service.Interface;
using FirstPro.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Repoistory
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext db;

        public GenericRepository(ApplicationDbContext _db)
        {
            db = _db;
        }


        public async Task<List<TEntity>> GetAsync()
        {
            var result = await db.Set<TEntity>().Select(x => x).ToListAsync();
            return result;

        }
        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            var result = await db.Set<TEntity>().Where(filter).ToListAsync();
            return result;

        }

        public async Task<List<TEntity>> GetAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = db.Set<TEntity>().Select(x => x);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }
        public async Task<TEntity> GetAsyncById(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {

            var query = db.Set<TEntity>().Where(filter);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public async Task CreateAsync(TEntity obj)
        {

            await db.Set<TEntity>().AddAsync(obj);
            await db.SaveChangesAsync();

        }
        public async Task UpdateAsync(TEntity obj)
        {

             db.Set<TEntity>().Update(obj);
            await db.SaveChangesAsync();


        }


        public async Task DeleteAsync(TEntity obj)
        {

            db.Set<TEntity>().Remove(obj);
            await db.SaveChangesAsync();

        }







    }
}
