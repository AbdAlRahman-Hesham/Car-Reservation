using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Specfications.ReservationSpecification
{
     public  class ReservationSpec : Specification<Reservation>
    {
        public ReservationSpec(string email):base(r => r.User.Email == email)
        {
            
        }
        //public ReservationSpec( string email , DateTime date):base(r => r.User.Email == email && r.StartDate == date)
        //{
            
        //}
        public ReservationSpec(int id) : base(r => r.Id == id)
        {
            
        }
    }
}
