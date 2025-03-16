using E_Commerce.DTOs.ErrorResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Car_Reservation_Domain.Entities.Identity;

namespace E_Commerce.APIs.Controllers;

public class BuggyController(CarRentContext context) : BaseApiController
{
    private readonly CarRentContext _context = context;

    [HttpGet("notfound")]
    public ActionResult GetNotFound()
    {
        return NotFound(new ApiResponse((int)HttpStatusCode.NotFound));
    }

    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        User product = null;

        var result = product.ToString();
        return Ok(result);
    }
    
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest();
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return Ok();
    }
}
