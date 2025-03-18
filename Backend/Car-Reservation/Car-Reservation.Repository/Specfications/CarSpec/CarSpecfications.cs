using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
namespace Car_Reservation.Repository.Specfications.CarSpec;

public class CarSpecfications: Specification<Car>
{
    public CarSpecfications() : base(DefaultIncludes)
    {
    }

    public CarSpecfications(Expression<Func<Car, bool>>? criteria, Expression<Func<Car, object>>? orderBy, SortOrder sortOrder, int? skip = 0, int? take = 10)
        : base(DefaultIncludes, criteria, orderBy, sortOrder, skip, take)
    {
    }
    public CarSpecfications(Expression<Func<Car, bool>>? criteria)
        : base(DefaultIncludes, criteria)
    {
    }



    public static CarSpecfications BuildCarSpecfication(string? sort, string? sortDirection, Expression<Func<Car, bool>>? criteria = null)
    {
        var (orderBy, sortOrderEnum) = GetSortingParameters(sort, sortDirection);
        return new CarSpecfications(criteria, orderBy, sortOrderEnum);
    }
    public static CarSpecfications BuildCarSpecfication(CarSpecParams CarSpec)
    {
        Expression<Func<Car, bool>>? criteria = GetCriteriaParameters(CarSpec.BrandId, CarSpec.ModelId, CarSpec.SearchName);
        var (orderBy, sortOrderEnum) = GetSortingParameters(CarSpec.Sort, CarSpec.SortOrder);
        var (skip, take) = GetPaginationParameters(CarSpec.PageSize, CarSpec.PageIndex);
        return new CarSpecfications(criteria, orderBy, sortOrderEnum, skip, take);
    }



    private static readonly List<Expression<Func<Car, object>>> DefaultIncludes = new() { c => c.Brand, c => c.Model };


    private static (Expression<Func<Car, object>>? OrderBy, SortOrder SortOrder)
        GetSortingParameters(string? sort, string? sortOrder)
    {
        if (string.IsNullOrEmpty(sort))
        {
            return (null, SortOrder.Ascending);
        }

        Expression<Func<Car, object>>? orderBy = sort.ToLower() switch
        {
            "name" => c => c.Name,
            "price" => c => c.Price,
            _ => null
        };

        if (orderBy == null)
        {
            return (null, SortOrder.Ascending);
        }

        var sortOrderEnum = (!string.IsNullOrEmpty(sortOrder) ? sortOrder.ToLower() : "asc") switch
        {
            "desc" => SortOrder.Descending,
            _ => SortOrder.Ascending
        };

        return (orderBy, sortOrderEnum);
    }
    private static Expression<Func<Car, bool>>? GetCriteriaParameters(int? brandId, int? categorityId, string? searchName)
    {
        Expression<Func<Car, bool>>? criteria = null;


        if (brandId.HasValue)
        {
            criteria = c => c.BrandId == brandId;
        }

        if (categorityId.HasValue)
        {
            if (criteria != null)
            {
                criteria = criteria.AndAlso(c => c.ModelId == categorityId);
            }
            else
            {
                criteria = c => c.ModelId == categorityId;
            }
        }
        if (!string.IsNullOrEmpty(searchName))
        {
            if (criteria != null)
            {
                criteria = criteria.AndAlso(c => c.Name.Contains(searchName));
            }
            else
            {
                criteria = c => c.Name.Contains(searchName);
            }
        }

        return criteria;
    }

    private static (int?, int?) GetPaginationParameters(int? PageSize, int? PageIndex)
    {
        if (PageSize.HasValue && PageIndex.HasValue)
        {
            int skip = (PageIndex.Value - 1) * PageSize.Value;
            return (skip, PageSize.Value);

        }

        return (null, null);
    }

}
