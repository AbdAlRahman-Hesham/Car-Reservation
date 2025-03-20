using Car_Reservation.APIs.Controllers;
using Car_Reservation.Dtos.ReviewDtos;
using Car_Reservation.DTOs.ErrorResponse;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.ServicesInterfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Car_Reservation_API.Controllers
{


    //    Get All Reviews For Car

    //    Get All Reviews For Use [TODO]

    //     Add Review For Car

    //     Delete Review For Car

    //      Update Review



    public class ReviewController : BaseApiController
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<User> _userManager;

        public ReviewController(IReviewService reviewService ,UserManager<User> userManager)
        {
            this._reviewService = reviewService;
            this._userManager = userManager;
        }

        [HttpGet("{carId}")]
        public async Task<ActionResult<IReadOnlyList<ReviewToReturnDto>>> GetAllReviewsForCar(int carId)
        {
            var reslut = await _reviewService.GetAllReviewsForCarAsync(carId);
            if (reslut == null) { return BadRequest(new ApiResponse(404)); }
            var reslutDto = reslut.Adapt<IReadOnlyList<ReviewToReturnDto>>();
            return Ok(reslutDto);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<ReviewToReturnDto>>> GetAllReviewsForUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail!);
            var reslut = await _reviewService.GetAllReviewsForUserAsync(user.Id);
            var reslutDto = reslut.Adapt<IReadOnlyList<ReviewToReturnDto>>();
            return Ok(reslutDto);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReviewToReturnDto>> CreateReview(ReviewDto reviewDto )
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
           var reslut = await _reviewService.AddReviewAsync(reviewDto.CarId, user.Id,reviewDto.Comment,reviewDto.Stars);
           var reslutDto = reslut.Adapt<ReviewToReturnDto>();
            return Ok(reslutDto);
        }
        //Delete Review For Car
        [HttpDelete("{ReviewId}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteReview(int ReviewId)
        {
            // get user 
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
      
            // get review 
            var review = await _reviewService.GetReviewByIdAsync(ReviewId);
            if (review == null) { return NotFound(new ApiResponse(404,"No Review With That Id")); }
            //check Access
            if (review.UserId != user.Id && !User.IsInRole("Admin") ) { return  Unauthorized(new ApiResponse(401, "You Are Not Authorized To Delete This Review")); }
            //delete review
            var reslut = await _reviewService.DeleteReviewAsync(review);
            if (reslut == 0) { return BadRequest(new ApiResponse(404,"Server Can not delete that review")); }
            return Ok(true);
        }
        [HttpGet("review/{reviewId}")]
        public async Task<ActionResult<ReviewToReturnDto>> GetReviewById(int reviewId)
        {
            var review = await _reviewService.GetReviewByIdAsync(reviewId);
            var reviewDto = review.Adapt<ReviewToReturnDto>();
            return reviewDto;
        }
        [HttpPut()]
        [Authorize]
        public async Task<ActionResult<ReviewToReturnDto>> UpdateReview(ReviewToUpdateDto reviewToUpdateDto)
        {
            // get user
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(userEmail);
            // get review
            var review = await _reviewService.GetReviewByIdAsync(reviewToUpdateDto.Id);
            if (review == null) { return NotFound(new ApiResponse(404, "No Review With That Id")); }
            //check Access
            if (review.UserId != user.Id && !User.IsInRole("Admin")) { return Unauthorized(new ApiResponse(401, "You Are Not Authorized To Update This Review")); }
            //update review
          
            var reslut = await _reviewService.UpdateReview(review, reviewToUpdateDto.Comment, reviewToUpdateDto.Stars);

            var reviewDtoToReturn = reslut.Adapt<ReviewToReturnDto>();
            return Ok(reviewDtoToReturn);
        }

    }
}
