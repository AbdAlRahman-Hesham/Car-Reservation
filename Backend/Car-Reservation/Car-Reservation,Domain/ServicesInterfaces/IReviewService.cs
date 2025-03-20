using Car_Reservation_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation_Domain.ServicesInterfaces
{
    public interface IReviewService
    {
        public Task<IReadOnlyList<Review>> GetAllReviewsForCarAsync(int carId);
        public Task<IReadOnlyList<Review>> GetAllReviewsForUserAsync(string userId);
        public Task<Review> AddReviewAsync(int carId, string userId, string comment, int stars);
        public  Task<Review?> GetReviewByIdAsync(int id);
        public Task<int> DeleteReviewAsync(Review review);
        public  Task<Review?> UpdateReview(Review review, string NewComment, int NewSrars);

    }
}
