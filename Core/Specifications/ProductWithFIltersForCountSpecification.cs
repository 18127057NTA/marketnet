using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFIltersForCountSpecification: BaseSpecification<Product>
    {
        public ProductWithFIltersForCountSpecification(ProductSpecPrams productPrams)
        :base(x => 
                //Search
                (string.IsNullOrEmpty(productPrams.Search) || x.Name.ToLower().Contains(productPrams.Search)) &&
                (!productPrams.StoreId.HasValue ||  x.StoreId == productPrams.StoreId) &&
                (!productPrams.TypeId.HasValue || x.ProductTypeId == productPrams.TypeId) &&
                (!productPrams.SupplierId.HasValue || x.Store.SupplierId == productPrams.SupplierId)
            )
        {
        }
    }
}