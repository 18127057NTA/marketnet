using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.Store)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        /*
        public async Task<IReadOnlyList<Product>> GetProductsByIdAsync(int id)
        {
             
            
        }
        */
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {

            return await _context.Products
                .Include(p => p.ProductType)
                .Include(p => p.Store)
                .Include(p => p.Store.Supplier)
                .ToListAsync();
            /*Lấy thông tin cần phải lấy
            */
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToArrayAsync();
        }

        public async Task<IReadOnlyList<Store>> GetStoresAsync()
        {
            return await _context.Stores
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Supplier>> GetSuppliersAsync()
        {
            return await _context.Suppliers.ToArrayAsync();
        }
    }
}