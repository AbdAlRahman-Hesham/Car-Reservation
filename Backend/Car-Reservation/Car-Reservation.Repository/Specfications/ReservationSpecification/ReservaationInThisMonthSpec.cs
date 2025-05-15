using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Specfications.ReservationSpecification
{
    public class ReservaationInThisMonthSpec : Specification<Reservation>
    {
        public ReservaationInThisMonthSpec() : base(
            r =>  r.StartDate.Month == DateTime.Now.Month && r.StartDate.Year == DateTime.Now.Year
            && r.EndDate.Month == DateTime.Now.Month && r.EndDate.Year == DateTime.Now.Year
            )
        {
            Includes.Add(r => r.Car);
        }
   
    }
}
