using Car_Reservation.Repository.Specifications;
using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Repository.Specfications.ReviewSpecification
{
  public   class ReviewWithReviewId : Specification<Review>
    {
        public ReviewWithReviewId(int reviewId):base(r => r.Id == reviewId)
        {
            
        }
    }
}
