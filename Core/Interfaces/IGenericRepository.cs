using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : Date // Không được dùng product controller với loại này
    {
        Task<T> GetByIdAsync(string id); //trước đó: int id
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        //Paging
        Task<int> CountAsync(ISpecification<T> spec);

        //OrderService
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}