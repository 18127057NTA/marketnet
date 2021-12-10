using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        //Phần này là làm riêng rẻ không type T, nhớ coi lịch sử github
        /*
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repo.GetProductsAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _repo.GetProductByIdAsync(id);
        }
        [HttpGet("stores")]
        public async Task<ActionResult<IReadOnlyList<Store>>> GetStores()
        {
            return Ok(await _repo.GetStoresAsync());
        }
        [HttpGet("suppliers")]
        public async Task<ActionResult<IReadOnlyList<Supplier>>> GetSuppliers()
        {
            return Ok(await _repo.GetSuppliersAsync());
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _repo.GetProductTypesAsync());
        }*/
        //Type T !!!!
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<Store> _storesRepo;
        private readonly IGenericRepository<ProductType> _typesRepo;
        private readonly IGenericRepository<Supplier> _suppliersRepo;
        private readonly IMapper _mapper;

        public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<Store> storesRepo,
            IGenericRepository<ProductType> typesRepo,
            IGenericRepository<Supplier> suppliersRepo,
            IMapper mappper
        )
        {
            _productsRepo = productsRepo;
            _storesRepo = storesRepo;
            _typesRepo = typesRepo;
            _suppliersRepo = suppliersRepo;
            _mapper = mappper;
        }

        [HttpGet]

        //Trước đó public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts(){}
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            //var products = await _productsRepo.ListAllAsync(); // repo type T

            // specification repo type T
            var spec = new ProductsWithTypesStoresSuppliers();
            var products = await _productsRepo.ListAsync(spec);

            //return Ok(products);// return không dto

            //return dạng Dto
            /*
            return products.Select(
                product => new ProductToReturnDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    ProductType = product.ProductType.Name,
                    ProductTypeId = product.ProductTypeId,
                    Store = product.Store.Name,
                    StoreId = product.StoreId,
                    StoreAddress = product.Store.Address,
                    SupplierId = product.Store.SupplierId,
                    SupplierName = product.Store.Supplier.Name,
                    Quantity = product.Quantity,
                    UnitPrice = product.UnitPrice,
                    PictureUrl = product.PictureUrl
                }
            ).ToList();*/

            //return Dto + mapping
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        //Chú ý loại trả về
        //public async Task<ActionResult<Product>> GetProduct(int id){} // Ban đầu
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            //return await _productsRepo.GetByIdAsync(id); // repo type T

            //repo type T spec
            var spec = new ProductsWithTypesStoresSuppliers(id);

            // return không dto
            //return await _productsRepo.GetEntityWithSpec(spec);

            // dto
            var product = await _productsRepo.GetEntityWithSpec(spec);

            //return dto
            /*
            return new ProductToReturnDto
            {
                Id = product.Id,
                Name = product.Name,
                ProductType = product.ProductType.Name,// Có thể lược bỏ nếu ko cần
                ProductTypeId = product.ProductTypeId,
                Store = product.Store.Name,
                StoreId = product.StoreId,// Có thể lược bỏ nếu ko cần
                StoreAddress = product.Store.Address,// Có thể lược bỏ nếu ko cần
                SupplierId = product.Store.SupplierId,// Có thể lược bỏ nếu ko cần
                SupplierName = product.Store.Supplier.Name,// Có thể lược bỏ nếu ko cần
                Quantity = product.Quantity,
                UnitPrice = product.UnitPrice,
                PictureUrl = product.PictureUrl
            };*/

            //return dto + mapping
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("stores")]
        public async Task<ActionResult<IReadOnlyList<Store>>> GetStores()
        {
            return Ok(await _storesRepo.ListAllAsync());
        }

        [HttpGet("suppliers")]
        public async Task<ActionResult<IReadOnlyList<Supplier>>> GetSuppliers()
        {
            return Ok(await _suppliersRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _typesRepo.ListAllAsync());
        }
    }

}