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
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUserContext _userContext;

        public BaseService(IGenericRepository<TEntity> repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.OwnerId = _userContext.UserId;
            return await _repository.AddAsync(entity);
        }


        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(e => e.Id == id && e.OwnerId == _userContext.UserId);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var existingEntity = await FindByIdAsync(entity.Id);

            if (existingEntity == null)
            {
                throw new InvalidOperationException("Entity not found or user not authorized to update.");
            }

            return await _repository.UpdateAsync(entity);
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await FindByIdAsync(id);

            if (entity == null)
            {
                throw new InvalidOperationException("Entity not found or user not authorized to delete.");
            }

            await _repository.RemoveAsync(entity);
        }

        public async Task<PagedList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> filter = null, int pageNumber = 1, int pageSize = 10)
        {
            Expression<Func<TEntity, bool>> userFilter = e => e.OwnerId == _userContext.UserId;

            if (filter != null)
            {
                userFilter = CombineExpressions(userFilter, filter);
            }

            var items = await _repository.ListAsync(userFilter, pageNumber, pageSize);
            return new PagedList<TEntity>(items, pageNumber, pageSize);
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
