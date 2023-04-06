using AutoMapper;
using MyMoneyAI.Application.DTOs;
using MyMoneyAI.Application.Interfaces;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace MyMoneyAI.Application.Services
{
    public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto> 
        where TEntity : BaseEntity
        where TDto : BaseDto
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUserContext _userContext;
        protected readonly IMapper _mapper;

        public BaseService(IGenericRepository<TEntity> repository, IUserContext userContext, IMapper mapper)
        {
            _repository = repository;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<TDto> AddAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);
            entity.OwnerId = _userContext.UserId;
            var newEntity = await _repository.AddAsync(entity);
            return _mapper.Map<TDto>(newEntity);
        }


        public async Task<TDto> FindByIdAsync(int id)
        {
            var entity =  await _repository.FindByIdAsync(e => e.Id == id && e.OwnerId == _userContext.UserId);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(TDto dto)
        {
            _ = await FindByIdAsync(dto.Id) ?? throw new InvalidOperationException("Entity not found or user not authorized to update.");
            TEntity entity = _mapper.Map<TEntity>(dto);
            var updatedEntity = await _repository.UpdateAsync(entity);
            return _mapper.Map<TDto>(updatedEntity);
        }

        public async Task RemoveAsync(int id)
        {
            var dto = await FindByIdAsync(id) ?? throw new InvalidOperationException("Entity not found or user not authorized to delete.");
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.RemoveAsync(entity);
        }

        public async Task<PagedList<TDto>> ListAsync(Expression<Func<TEntity, bool>> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            Expression<Func<TEntity, bool>> userFilter = e => e.OwnerId == _userContext.UserId;

            if (filter != null)
            {
                userFilter = CombineExpressions(userFilter, filter);
            }

            var items = await _repository.ListAsync(userFilter, pageNumber, pageSize);
            var itemsDto = _mapper.Map<IEnumerable<TDto>>(items);
            return new PagedList<TDto>(itemsDto, pageNumber, pageSize);
        }

        private static Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var combined = new ReplaceParameterVisitor(second.Parameters[0], parameter)
                .VisitAndConvert(second.Body, nameof(CombineExpressions));

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(first.Body, combined), parameter);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParameter ? _newParameter : base.VisitParameter(node);
            }
        }


    }
}
