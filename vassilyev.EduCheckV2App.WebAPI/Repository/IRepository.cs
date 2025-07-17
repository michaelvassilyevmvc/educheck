using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Repository;

public interface IRepository<T> where T: class
{
    Task<ICollection<T>> GetAllAsync();
    Task<T> GetAsync(Guid id);
    Task CreateAsync(T item);
    void Update(T item);
    void Remove(T item);
    Task SaveAsync();
}