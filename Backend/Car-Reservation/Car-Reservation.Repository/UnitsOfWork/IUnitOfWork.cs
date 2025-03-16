using Car_Reservation_Domain.Entities;
using Car_Reservation.Repository.Reprositories_Interfaces;

namespace Car_Reservation.Repository.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable 
{
    IGenaricRepository<T> Repository<T>() where T : BaseEntity;
    public Task<int> CompleteAsync();
}
