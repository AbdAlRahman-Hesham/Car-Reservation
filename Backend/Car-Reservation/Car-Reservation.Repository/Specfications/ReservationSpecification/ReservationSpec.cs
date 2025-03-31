using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Specfications.ReservationSpecification
{
    public  class ReservationSpec : Specification<Reservation>
    {
        public ReservationSpec(string email):base(r => r.User.Email == email)
        {
            
        }
        public ReservationSpec(string email , DateTime Date): base(r => r.User.Email == email && r.StartDate <= Date && r.EndDate >= Date)
        {
                //.Where(reservation => reservation.StartDate <= date && reservation.EndDate >= date)


        }

        public ReservationSpec(int id) : base(r => r.Id == id)
        {
            
        }
        public ReservationSpec(Expression<Func<Reservation,bool>> criteria) :base(DefaultIncludes, criteria)
        {
            
        }
        public ReservationSpec(int carId, DateTime startDate, DateTime endDate):base(r => r.CarId == carId && (( r.StartDate >= startDate && r.StartDate<= endDate)||( r.EndDate <= endDate&&r.EndDate>=startDate)))
        {
            
        }
        public ReservationSpec(int carId, DateTime date): base(r => r.CarId == carId && r.StartDate <= date && r.EndDate >= date)
        {
          //  var reslut = carReservations.Where(c=>c.StartDate<=date && c.EndDate>=date).SingleOrDefault();

        }
        
        public ReservationSpec() : base(DefaultIncludes)
        {
        }
        private static readonly List<Expression<Func<Reservation, object>>> DefaultIncludes = new() { r=>r.User,r=>r.Car };

    }
}
