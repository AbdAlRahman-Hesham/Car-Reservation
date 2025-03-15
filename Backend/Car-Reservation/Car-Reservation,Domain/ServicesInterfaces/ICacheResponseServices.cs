namespace E_Commerce.Domain.ServicesInterfaces;

public interface ICacheResponseService
{
    Task SetObjectInMemoryAsync(string key, object value, TimeSpan lifeTime);
    Task<string?> GetObjectInMemoryAsync(string key);

}
