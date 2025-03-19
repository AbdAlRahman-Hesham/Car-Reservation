using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities.CarEntity;
using System.Linq.Expressions;
namespace Car_Reservation.Repository.Specfications.CarSpec;

public class BrandSpecfication : Specification<Brand>
{
    private static readonly List<Expression<Func<Brand, object>>> DefaultIncludes = new() { b => b.Models};
    public BrandSpecfication():base(DefaultIncludes)
    {
        
    }
    public BrandSpecfication(int id):base(DefaultIncludes, b=>b.Id==id)
    {
        
    }


}