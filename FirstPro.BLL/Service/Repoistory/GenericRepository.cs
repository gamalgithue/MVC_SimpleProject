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

        #region prop
        private readonly ApplicationDbContext db;
        private DbSet<TEntity> dbSet;
        #endregion


        #region ctor
        public GenericRepository(ApplicationDbContext _db)
        {
            this.db = _db;
            this.dbSet = _db.Set<TEntity>();
        }
        #endregion



        #region Actions
        public async Task<IEnumerable<TEntity>> GetAsync(
           Expression<Func<TEntity, bool>>? filter = null,
           int? page = null,
           int pageSize = 10,
           bool noTrack = false,
           params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (noTrack)
            {
                query = query.AsNoTracking();
            }

            //if (orderBy != null)
            //{
            //    query = orderBy(query);
            //}

            if (page.HasValue && page > 0)
            {
                query = query.Skip((page.Value - 1) * pageSize).Take(pageSize);
            }


            return await query.ToListAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            bool noTrack = false,
             params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (noTrack)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task CreateOrUpdateAsync(TEntity entity)
        {

            var existingEntity = await dbSet.FindAsync(GetKeyValues(entity));

            if (existingEntity != null)
            {
                db.Entry(existingEntity).CurrentValues.SetValues(entity); // edit
                await db.SaveChangesAsync();
            }
            else
            {
                await dbSet.AddAsync(entity); // add
                await db.SaveChangesAsync();
            }

        }


        // Soft delete
        public async Task DeleteAsync(TEntity entity)
        {
            //await CreateOrUpdateAsync(entity);

            // if delete
            dbSet.Remove(entity);
            await db.SaveChangesAsync();
        }

        public object[] GetKeyValues(TEntity entity)
        {
            var entityType = db.Model.FindEntityType(typeof(TEntity));
            var key = entityType.FindPrimaryKey();
            var keyValues = key.Properties.Select(p => p.PropertyInfo.GetValue(entity)).ToArray();
            return keyValues;
        }



        #endregion
    }
}




