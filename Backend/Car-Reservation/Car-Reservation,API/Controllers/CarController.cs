using Car_Reservation.APIs.Controllers;
using Car_Reservation.Dtos.CarDtos;
using Car_Reservation.DTOs.ErrorResponse;
using Car_Reservation.DTOs.Pagination;
using Car_Reservation.Repository.Specfications.CarSpec;
using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities.CarEntity;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car_Reservation_API.Controllers;

public class CarController(IUnitOfWork unitOfWork) : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    //Get All Cars
    [HttpGet]
    public async Task<ActionResult<PaginationDto<CarToReturnDto>>> GetAllCars([FromQuery] CarSpecParams @params)
    {
        var specification = new CarSpecfications();
        specification = CarSpecfications.BuildCarSpecfication(@params);
        var result = await _unitOfWork.Repository<Car>().GetCollectionOfAllAsyncWithSpecification(specification);
        int count = await _unitOfWork.Repository<Car>().GetCountAsync(specification.Criteria);
        return Ok(new PaginationDto<CarToReturnDto>(@params.PageSize!.Value, @params.PageIndex!.Value, count, result.Adapt<ICollection<CarToReturnDto>>()));
    }

    //Get Car By Id
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CarToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    public async Task<ActionResult<CarToReturnDto>> GetCar(int id)
    {
        var specification = new CarSpecfications(c => c.Id == id);
        var result = await _unitOfWork.Repository<Car>().GetAsyncWithSpecification(specification);

        if (result == null)
            return NotFound(new ApiResponse(404, "No car with this id"));

        return Ok(result.Adapt<CarToReturnDto>());
    }

    //Get All Brands
    [HttpGet("/api/Brands")]
    public async Task<ActionResult<IReadOnlyList<BrandToReturnDto>>> GetAllBrands()
    {
        var result = await _unitOfWork.Repository<Brand>().GetAllAsync();

        return Ok(result.Adapt<IReadOnlyList<BrandToReturnDto>>());
    }



    // Get All Models For Brand
    [HttpGet("/api/Models")]
    [ProducesResponseType(typeof(CarToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    public async Task<ActionResult<ICollection<Model>>> GetAllModels(int brandId)
    {
        var specfication = new BrandSpecfication(brandId);
        var brand = await _unitOfWork.Repository<Brand>().GetAsyncWithSpecification(specfication);
        if (brand is null)
            return NotFound(new ApiResponse(404, "No brand with this id"));

        var result = brand.Models;

        return Ok(result.Adapt<ICollection<ModelToReturnDto>>());

    }

    // Add Car (Admin)
    [HttpPost]
    [Authorize(Roles ="Admin")]
    [ProducesResponseType(typeof(CarToReturnDto), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    public async Task<ActionResult<CarToReturnDto>> AddCar([FromBody] CarDto carDto)
    {
        if (carDto == null)
            return BadRequest(new ApiResponse(400, "Invalid car data"));

        var car = carDto.Adapt<Car>();
        await _unitOfWork.Repository<Car>().AddAsync(car);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car.Adapt<CarToReturnDto>());
    }

    // Update Car Info (Admin)
    [HttpPut("{id}")]
    [Authorize(Roles ="Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    public async Task<ActionResult> UpdateCar(int id, [FromBody] CarDto carDto)
    {
        if (carDto == null)
            return BadRequest(new ApiResponse(400, "Invalid car data"));

        var car = await _unitOfWork.Repository<Car>().GetAsync(id);
        if (car == null)
            return NotFound(new ApiResponse(404, "No car with this id"));

        carDto.Adapt(car); // Update the existing car entity
        _unitOfWork.Repository<Car>().Update(car);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    // Delete Car (Admin)
    [HttpDelete("{id}")]
    [Authorize(Roles ="Admin")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ApiResponse), 404)]
    public async Task<ActionResult> DeleteCar(int id)
    {
        var car = await _unitOfWork.Repository<Car>().GetAsync(id);
        if (car == null)
            return NotFound(new ApiResponse(404, "No car with this id"));

        _unitOfWork.Repository<Car>().Delete(car);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }


}

