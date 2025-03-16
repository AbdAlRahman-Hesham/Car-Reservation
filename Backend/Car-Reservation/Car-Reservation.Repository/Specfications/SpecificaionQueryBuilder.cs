using Car_Reservation_Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Car_Reservation.Repository.Specifications;

public static class SpecificaionQueryBuilder
{
    public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> inputQuery, Specification<TEntity> specification) where TEntity : BaseEntity
    {

        if (specification.Criteria is not null)
            inputQuery = inputQuery.Where(specification.Criteria);

        if (specification.Includes is not null && specification.Includes.Count > 0)
            inputQuery = specification.Includes.
                Aggregate(inputQuery, (current, include) => current.Include(include));

        if (specification.OrderBy is not null && specification.SortOrder == SortOrder.Ascending)
            inputQuery = inputQuery.OrderBy(specification.OrderBy);

        else if (specification.OrderBy is not null && specification.SortOrder == SortOrder.Descending)
            inputQuery = inputQuery.OrderByDescending(specification.OrderBy);

        inputQuery = inputQuery.Skip(specification.Skip!.Value).Take(specification.Take!.Value);

        return inputQuery;
    }
}
