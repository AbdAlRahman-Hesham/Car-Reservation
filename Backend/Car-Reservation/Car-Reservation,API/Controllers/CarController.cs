using Car_Reservation.APIs.Controllers;
using Car_Reservation.Repository.Reprositories_Interfaces;
using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities.CarEntity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Reservation_API.Controllers;

public class CarController(IUnitOfWork unitOfWork) : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGenaricRepository<Car> _carRepo = unitOfWork.Repository<Car>();
    //Get All Cars

    //Get Car By Id
    //Get Car By Category
    //Add Car ---> Admin
    //Delete Car --> Admin
    //Update Car Info Car --> Admin
    //Get All Brand

    // Get All Model For Brand
}
