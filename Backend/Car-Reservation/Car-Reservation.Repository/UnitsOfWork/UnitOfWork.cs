using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using E_Commerce.Domain.Entities;
using E_Commerce.Repository.Reprositories;
using E_Commerce.Repository.Reprositories_Interfaces;
using System.Collections;

namespace E_Commerce.Repository.UnitOfWork;

public class UnitOfWork(CarRentContext context) : IUnitOfWork
{
    private readonly CarRentContext _context = context;
    private readonly Hashtable _repositories = new Hashtable();

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }

    public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        var type = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenaricRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
            _repositories.Add(type, repositoryInstance);
        }
        return (IGenaricRepository<TEntity>)_repositories[type]!;
    }
}
