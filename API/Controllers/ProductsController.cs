using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController(StoreContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {

            //var products = _context.Products.ToList();

            var products = await (from p in _context.Products
                            join t in _context.ProductTypes on p.t_type.Id equals t.Id
                            join st in _context.Stores on p.t_storeid.Id equals st.Id
                            join spl in _context.Suppliers on st.t_supplier.Id equals spl.Id
                            select new
                            {
                                ProductId = p.Id,
                                ProductName = p.Name,
                                ProductType = t.Name,
                                StoreName = st.Name,
                                StoreAddress = st.Address,
                                SupplierName = spl.Name,
                                ProductQuantity = p.Quantity,
                                ProductUnitPrice = p.UnitPrice
                            }).ToListAsync();

            /*Lấy thông tin cần phải lấy

            */

            return Ok(products);
        }

        [HttpGet("{id}")]
        //public async Task<ActionResult<Product>>>  GetProduct(int id){return await _context.Products.FindAsync(id);}
        public async Task<ActionResult<List<Product>>> GetProduct(int id)
        {
            
            var products = await (from p in _context.Products
                            join t in _context.ProductTypes on p.t_type.Id equals t.Id
                            join st in _context.Stores on p.t_storeid.Id equals st.Id
                            join spl in _context.Suppliers on st.t_supplier.Id equals spl.Id
                            where p.Id == id
                            select new
                            {
                                ProductId = p.Id,
                                ProductName = p.Name,
                                ProductType = t.Name,
                                StoreName = st.Name,
                                StoreAddress = st.Address,
                                SupplierName = spl.Name,
                                ProductQuantity = p.Quantity,
                                ProductUnitPrice = p.UnitPrice
                            }).ToListAsync();

            return Ok(products);
        }
    }
}