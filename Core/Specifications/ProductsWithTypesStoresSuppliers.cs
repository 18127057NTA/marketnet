using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesStoresSuppliers : BaseSpecification<Product>
    {
        public ProductsWithTypesStoresSuppliers()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Store);
            AddInclude(x => x.Store.Supplier);
        }

        public ProductsWithTypesStoresSuppliers(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Store);
            AddInclude(x => x.Store.Supplier);
        }
    }
}