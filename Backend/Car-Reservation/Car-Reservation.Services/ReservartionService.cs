using Car_Reservation.Repository.UnitOfWork;
using Car_Reservation_Domain.Entities;
using Car_Reservation_Domain.ServicesInterfaces;
using Car_Reservation.Repository.Specfications.ReservationSpecification;
using Car_Reservation_Domain.Entities.CarEntity;

namespace Car_Reservation.Services;

public class ReservationService : IReservationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISendEmail _sendEmail;

    public ReservationService(IUnitOfWork unitOfWork, ISendEmail sendEmail)
    {
        _unitOfWork = unitOfWork;
        _sendEmail = sendEmail;
    }
    public async Task<Reservation?> CancleReservation(Reservation result)
    {
        result.Status = ReservationStatus.Cancelled;
        _unitOfWork.Repository<Reservation>().Update(result);
        await _unitOfWork.CompleteAsync();
        return result;
    }
    public async Task<IReadOnlyList<Reservation>?> GetAllReservationsForUser(string email)
    {
        var spec = new ReservationSpec(email);
        var reservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
        return reservations;
    }

    public async Task<IReadOnlyList<Reservation>?> GetAllReservationsForUserByDate(string userEmail, DateTime date)
    {
        var spec = new ReservationSpec(userEmail, date);
        var userReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
        return userReservations;
    }

    public async Task<Reservation?> GetReservationById(int id)
    {
        var spec = new ReservationSpec(id);
        var reservation = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(spec);
        return reservation;
    }

    public async Task<IReadOnlyList<Reservation>?> GetAllReservationsForCar(int carId)
    {
        var spec = new ReservationSpecWithCarId(carId);
        var reservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
        return reservations;
    }

    public async Task<Reservation?> GetReservationForCarByDate(int carId, DateTime date)
    {
        var spec = new ReservationSpec(carId, date);
        var result = await _unitOfWork.Repository<Reservation>().GetAsyncWithSpecification(spec);
        return result;
    }

    public async Task<IReadOnlyList<Reservation>?> GetCarReservationsByDates(int carId, DateTime startDate, DateTime endDate)
    {
        var spec = new ReservationSpec(carId, startDate, endDate);
        var carReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);
        return carReservations;
    }

    public async Task<Reservation?> MakeReservationForUser(string userId, DateTime startDate, DateTime endDate, int carId)
    {
        // Check if we have that car in database
        var car = await _unitOfWork.Repository<Car>().GetAsync(carId);
        if (car == null) { return null; } // Car not found

        // Check if the car is available
        var spec = new ReservationSpecWithCarId(carId, startDate, endDate);
        var carReservationsInRequiredDate = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(spec);

        // Check if there is a free date between
        if (carReservationsInRequiredDate.Count > 0) { return null; } // Car is not available

        // Create the reservation
        var reservation = new Reservation
        {
            CarId = carId,
            StartDate = startDate,
            EndDate = endDate,
            UserId = userId,
            Status = ReservationStatus.Pending,
        };

        await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
        await _unitOfWork.CompleteAsync();
        return reservation;
    }

    public async Task<Reservation?> CancelReservation(Reservation result)
    {
        result.Status = ReservationStatus.Cancelled;
        _unitOfWork.Repository<Reservation>().Update(result);
        await _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<bool> IsCarExist(int carId)
    {
        var car = await _unitOfWork.Repository<Car>().GetAsync(carId);
        return car != null;
    }

    public async Task AutoCancelStaleReservations()
    {
        var now = DateTime.UtcNow;
        var staleReservations = await _unitOfWork.Repository<Reservation>().GetAllAsyncWithSpecification(
            new StaleReservationSpecification(now.AddHours(-1))
        );

        if (staleReservations.Any())
        {
            foreach (var reservation in staleReservations)
            {
                reservation.Status = ReservationStatus.Cancelled;
                var model = await _unitOfWork.Repository<Model>().GetAsync(reservation.Car.ModelId);
                await _sendEmail.SendReservationCanceledEmailAsync(reservation, model!.Name);
                _unitOfWork.Repository<Reservation>().Update(reservation);
            }

            await _unitOfWork.CompleteAsync();
        }
    }
}