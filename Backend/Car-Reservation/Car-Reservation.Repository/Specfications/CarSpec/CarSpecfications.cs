using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.Data.SqlClient;
using System.Linq.Expressions;
namespace Car_Reservation.Repository.Specfications.CarSpec;

public class CarSpecfications: Specification<Car>
{
    private static readonly List<Expression<Func<Car, object>>> DefaultIncludes =
        new() { c=>c.Brand,c=>c.Model };
    
    public CarSpecfications(): base(DefaultIncludes) {}

    public CarSpecfications(Expression<Func<Car, bool>>? criteria) : 
        base(DefaultIncludes,criteria) {}
    public CarSpecfications(Expression<Func<Car, bool>>? criteria, Expression<Func<Car, object>>? orderBy, SortOrder sortOrder, int? skip = 0, int? take = 10)
        : base(DefaultIncludes, criteria, orderBy, sortOrder, skip, take)
    {
    }

}
