using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Car_Reservation_Domain.Entities;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Car_Reservation.Repository.Specifications;
using Car_Reservation.Repository.Reprositories_Interfaces;

namespace Car_Reservation.Repository.Reprositories;

public class GenaricRepository<T>(CarRentDbContext db) : IGenaricRepository<T> where T : BaseEntity
{
    private protected readonly CarRentDbContext _db = db;

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
