using E_Commerce.Domain.Entities;
using E_Commerce.Repository.Reprositories_Interfaces;

namespace E_Commerce.Repository.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable 
{
    IGenaricRepository<T> Repository<T>() where T : BaseEntity;
    public Task<int> CompleteAsync();
}
