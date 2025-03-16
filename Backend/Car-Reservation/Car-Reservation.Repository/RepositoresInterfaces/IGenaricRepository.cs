using Car_Reservation_Domain.Entities;
using Car_Reservation.Repository.Specifications;
using System.Linq.Expressions;

namespace Car_Reservation.Repository.Reprositories_Interfaces;

public interface IGenaricRepository<T> where T : BaseEntity
{
    public Task<T?> GetAsync(int id);
    public Task<T?> GetAsyncWithSpecification(Specification<T> specification);
    public Task<IReadOnlyList<T>> GetAllAsync();
    public Task<IReadOnlyList<T>> GetAllAsyncWithSpecification(Specification<T> specification);
    public Task<ICollection<T>> GetCollectionOfAllAsyncWithSpecification(Specification<T> specification);
    public Task<int> GetCountAsync(Expression<Func<T, bool>>? Criteria);
    public Task AddAsync(T entity);
    public void Update(T entity);
    public void Delete(T entity);
}
