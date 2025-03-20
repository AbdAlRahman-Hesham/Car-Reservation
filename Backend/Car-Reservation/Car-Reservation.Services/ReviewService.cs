using Car_Reservation.Dtos.ReviewDtos;
using Car_Reservation.Repository.Specfications.ReviewSpecification;
using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car_Reservation.Services
{
    public  class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyList<Review>> GetAllReviewsForCarAsync(int carId)
        {
            var spec = new ReviewWithId(carId);
            var reviews = await _unitOfWork.Repository<Review>().GetAllAsyncWithSpecification(spec);
            return  reviews;
        }
        public async Task<IReadOnlyList<Review>> GetAllReviewsForUserAsync(string userId)
        {
            var spec = new ReviewWithId(userId);
            var reviews = await _unitOfWork.Repository<Review>().GetAllAsyncWithSpecification(spec);
            return reviews;
        }
        public async Task<Review> AddReviewAsync(int carId , string userId , string comment , int stars)
        {
            var review = new Review
            {
                CarId = carId,
                UserId = userId,
                Comment = comment,
                Stars = stars
            };
            await _unitOfWork.Repository<Review>().AddAsync(review);
            await _unitOfWork.CompleteAsync();
            return review;
        }
        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            var spec = new ReviewWithReviewId(id);
            var review = await _unitOfWork.Repository<Review>().GetAsyncWithSpecification(spec);
            return review;
        }
        public async Task<int> DeleteReviewAsync(Review review)
        {
            _unitOfWork.Repository<Review>().Delete(review);
            return await _unitOfWork.CompleteAsync();
        }
        public async Task<Review?> UpdateReview(Review review,string NewComment, int NewSrars)
        {
           review.Comment = NewComment;
            review.Stars = NewSrars;
            _unitOfWork.Repository<Review>().Update (review);
            await _unitOfWork.CompleteAsync();
            return review;
        }
       
    }
}
