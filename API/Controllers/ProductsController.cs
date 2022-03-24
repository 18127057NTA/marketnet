using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Entities.Responses;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;


namespace API.Controllers
{
    //Lúc chưa có BaseApiController
    //[ApiController]
    //[Route("api/[controller]")]
    public class ProductsController : BaseApiController //ControllerBase
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

        //Vaccine
        private readonly VaccineRepository _vaccineRepository;

        public ProductsController(
            IGenericRepository<Product> productsRepo,
            IGenericRepository<Store> storesRepo,
            IGenericRepository<ProductType> typesRepo,
            IGenericRepository<Supplier> suppliersRepo,
            IMapper mappper,

            //Vaccine
            VaccineRepository vaccineRepository
        )
        {
            _productsRepo = productsRepo;
            _storesRepo = storesRepo;
            _typesRepo = typesRepo;
            _suppliersRepo = suppliersRepo;
            _mapper = mappper;

            //Vaccine
            _vaccineRepository = vaccineRepository;
        }

        [HttpGet]

        //Trước đó public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts(){}
        //Trước đó public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(){}
        //Trước đó Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort){}
        //Trước đó public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts{}

        // 2022-03-22 8:53 ------------ MỚI COMMENT -----------
        //public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts
        public async Task<ActionResult> GetProducts
        (
            //Trước đó
            /*
            string sort,
            int? storeId,
            int? typeId,
            int? supplierId*/

            [FromQuery] ProductSpecPrams productPrams
        )
        {
            //---------------THUẦN SQL: KHÔNG ĐƯỢC EDIT-------------

            //var products = await _productsRepo.ListAllAsync(); // repo type T

            // specification repo type T
            // 2022-03-22 8:53 ------------ MỚI COMMENT -----------
            //var spec = new ProductsWithTypesStoresSuppliers(/*sort, storeId, typeId, supplierId*/ productPrams); //thay đổi theo Mongo

            // paging count
            // 2022-03-22 8:53 ------------ MỚI COMMENT -----------
            //var countSpec = new ProductWithFIltersForCountSpecification(productPrams);
            // 2022-03-22 8:53 ------------ MỚI COMMENT -----------
            //var totalItems = await _productsRepo.CountAsync(countSpec); //thay đổi theo Mongo 

            // 2022-03-22 8:53 ------------ MỚI COMMENT -----------
            //var products = await _productsRepo.ListAsync(spec); //thay đổi theo Mongo

            //Paging
            // 2022-03-22 8:53 ------------ MỚI COMMENT -----------
            //var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products); //thay đổi theo Mongo

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
            //return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));

            // 2022-03-22 8:53 ------------ MỚI COMMENT -----------
            //return Ok(new Pagination<ProductToReturnDto>(productPrams.PageIndex, productPrams.PageSize, totalItems, data));

            //----------------SƯA THEO MONGODB --------------
            /*var client = new MongoClient("mongodb://localhost:27017");
            var db = client.GetDatabase("vnvc-demo");
            var collection = db.GetCollection<BsonDocument>("vaccine-info");
            var result = collection.Find(new BsonDocument()).ToList(); // Lỗi ở đây

            //Test
            Console.WriteLine(result[0]);

            //ProductToReturnDto -> VaccineToReturnDto*/

            var vaccines = await _vaccineRepository.GetVaccinesAsync();
            
            return Ok(new VaccineReponse(productPrams.PageIndex, productPrams.PageSize, 100, vaccines)); // đang test với random pageIndex, pageSize, count
        }

        [HttpGet("{id}")]
        //Chú ý loại trả về
        //public async Task<ActionResult<Product>> GetProduct(int id){} // Ban đầu
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
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
            if (product == null) return NotFound(new ApiResponse(404));
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