using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesStoresSuppliers : BaseSpecification<Product>
    {
        //Trước đó: public ProductsWithTypesStoresSuppliers()
        //Trước đó: public ProductsWithTypesStoresSuppliers(string sort){}
        public ProductsWithTypesStoresSuppliers(/*string sort, int? storeId, int? typeId, int? supplierId*/ ProductSpecPrams productPrams)
            :base(x => 
                //Search
                (string.IsNullOrEmpty(productPrams.Search) || x.Name.ToLower().Contains(productPrams.Search)) &&
                (!productPrams.StoreId.HasValue ||  x.StoreId == productPrams.StoreId) &&
                (!productPrams.TypeId.HasValue || x.ProductTypeId == productPrams.TypeId) &&
                (!productPrams.SupplierId.HasValue || x.Store.SupplierId == productPrams.SupplierId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Store);
            AddInclude(x => x.Store.Supplier);
            //Sorting
            AddOrderBy(x => x.Name);
            //Paging
            ApplyPaging(productPrams.PageSize * (productPrams.PageIndex - 1), productPrams.PageSize);
            //AddOrderBy(x => x.CreatedDate); //Chú ý bên dưới có thể ghép vô không?
            if (!string.IsNullOrEmpty(productPrams.Sort))
            {
                switch(productPrams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.UnitPrice);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.UnitPrice);
                        break;
                    case "createdDateAsc":
                        AddOrderBy(p => p.CreatedDate);
                        break;
                    case "createdDateDesc":
                        AddOrderByDescending(p=>p.CreatedDate);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }

        }

        public ProductsWithTypesStoresSuppliers(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.Store);
            AddInclude(x => x.Store.Supplier);
        }
    }
}