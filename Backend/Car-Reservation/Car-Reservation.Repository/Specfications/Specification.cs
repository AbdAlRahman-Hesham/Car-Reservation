using E_Commerce.Domain.Entities;
using Microsoft.Data.SqlClient;

using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace E_Commerce.Repository.Specifications;

public abstract class Specification<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public ReadOnlyCollection<Expression<Func<TEntity, object>>> Includes { get; }
    public Expression<Func<TEntity, object>>? OrderBy { get; }
    public SortOrder SortOrder { get; }
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 10;


    protected Specification()
    {
        Includes = new List<Expression<Func<TEntity, object>>>().AsReadOnly();
    }

    protected Specification(Expression<Func<TEntity, bool>>? criteria) : this()
    {
        Criteria = criteria;
    }

    

    protected Specification(
        List<Expression<Func<TEntity, object>>> includes,
        Expression<Func<TEntity, bool>>? criteria = null,
        Expression<Func<TEntity, object>>? orderBy = null,
        SortOrder? sortOrder = SortOrder.Ascending,
        int? skip = 0,
        int? take = 10
        )
    {
        Criteria = criteria;
        OrderBy = orderBy;
        SortOrder = sortOrder?? SortOrder.Ascending;
        Includes = includes.AsReadOnly();
        Skip = skip;
        Take = take;
    }
}
