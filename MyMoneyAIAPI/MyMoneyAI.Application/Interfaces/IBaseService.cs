using MyMoneyAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace MyMoneyAI.Application.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<PagedList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null, int pageNumber = 1, int pageSize = 10);
        Task<TEntity> FindByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task RemoveAsync(int id);
    }
}
