using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstPro.BLL.Service.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<List<TEntity>> GetAsync();

        public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter);
        public Task<List<TEntity>> GetAsync(params Expression<Func<TEntity, object>>[] includes);

        public Task<TEntity> GetAsyncById(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes);
        public Task CreateAsync(TEntity obj);
        public Task UpdateAsync(TEntity obj);
        public Task DeleteAsync(TEntity obj);
    }


}

