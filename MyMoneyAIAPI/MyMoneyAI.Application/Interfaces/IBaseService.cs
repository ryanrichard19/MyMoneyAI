using MyMoneyAI.Application.DTOs;
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
    public interface IBaseService<TEntity,TDto> where TEntity : BaseEntity where TDto : BaseDto
    {
        Task<PagedList<TDto>> ListAsync(Expression<Func<TEntity, bool>> filter = null, int pageNumber = 1, int pageSize = 10);
        Task<TDto> FindByIdAsync(int id);
        Task<TDto> AddAsync(TDto dto);
        Task<TDto> UpdateAsync(TDto dto);
        Task RemoveAsync(int id);
    }
}
