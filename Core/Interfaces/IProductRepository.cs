using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        
        //Task<IReadOnlyList<Product>> GetProductsByIdAsync(int id);
        
        //Phần này làm riêng lẻ, không gộp thành generic type
        /*
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<IReadOnlyList<Supplier>> GetSuppliersAsync();
        Task<IReadOnlyList<Store>> GetStoresAsync();*/

        
    }
}