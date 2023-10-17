using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SpecificationPatternInEfCore.Entity;

namespace SpecificationPatternInEfCore.Specifications;

public abstract class Specification<TEntity>
    where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Expression<Func<TEntity, object>>>? Includes { get; } = new();
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
    protected Specification()
    {

    }
    protected Specification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;
    }

    protected void AddIncludes(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes?.Add(includeExpression);
    }

    protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }
}

public static class SpecificationBuilder
{
    public static IQueryable<TEntity> GetQuery<TEntity>
        (IQueryable<TEntity> inputQuery,
            Specification<TEntity> specification)
     where TEntity : BaseEntity
    {
        var query = inputQuery;
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        if (specification.Includes != null)
        {
            query = specification.Includes
                .Aggregate(query, (current, include)
                    => current.Include(include));
        }

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }
        return query;
    }
}