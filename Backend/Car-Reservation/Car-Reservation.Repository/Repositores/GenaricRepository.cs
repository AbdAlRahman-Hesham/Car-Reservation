using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using E_Commerce.Domain.Entities;
using E_Commerce.Repository.Reprositories_Interfaces;
using E_Commerce.Repository.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_Commerce.Repository.Reprositories;

public class GenaricRepository<T>(CarRentContext db) : IGenaricRepository<T> where T : BaseEntity
{
    private protected readonly CarRentContext _db = db;

    public async Task<T?> GetAsync(int id)=> await _db.Set<T>().FindAsync(id);
    public async Task<T?> GetAsyncWithSpecification(Specification<T> specification)
            => await _db.Set<T>().GetQuery<T>(specification).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<T>> GetAllAsync()=> await _db.Set<T>().ToListAsync();
    public async Task<IReadOnlyList<T>> GetAllAsyncWithSpecification(Specification<T> specification) => 
        await _db.Set<T>().GetQuery<T>(specification).ToListAsync();
    public async Task<ICollection<T>> GetCollectionOfAllAsyncWithSpecification(Specification<T> specification) => 
        await _db.Set<T>().GetQuery<T>(specification).ToListAsync();

    public async Task<int> GetCountAsync(Expression<Func<T, bool>>? Criteria)
    {
        if (Criteria == null)
            return await _db.Set<T>().CountAsync();
        return await _db.Set<T>().Where(Criteria).CountAsync();
    }

    public async Task AddAsync(T entity) => await _db.AddAsync(entity);

    public void Update(T entity) =>  _db.Update(entity);


    public void Delete(T entity) => _db.Remove(entity);
}
