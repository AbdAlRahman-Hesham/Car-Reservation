using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Specfications.ReviewSpecification
{
   public  class ReviewWithId : Specification<Review>
    {
        public ReviewWithId(int carId):base(r => r.CarId == carId)
        {
            Includes.Add(r => r.User);

        }
        public ReviewWithId(string userId): base(r => r.UserId == userId)
        {
            
        }
    }
}
