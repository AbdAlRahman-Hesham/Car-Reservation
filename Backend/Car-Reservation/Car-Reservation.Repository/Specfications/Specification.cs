﻿using Car_Reservation_Domain.Entities;
using Microsoft.Data.SqlClient;

using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Car_Reservation.Repository.Specifications;

public  class Specification<TEntity> where TEntity : BaseEntity
{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; }
    public Expression<Func<TEntity, object>>? OrderBy { get; }
    public SortOrder SortOrder { get; }
    public int? Skip { get; set; } = 0;
    public int? Take { get; set; } = 10;


    public Specification()
    {
        Includes = new List<Expression<Func<TEntity, object>>>();
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
        Includes = includes;
        Skip = skip;
        Take = take;
    }
}
