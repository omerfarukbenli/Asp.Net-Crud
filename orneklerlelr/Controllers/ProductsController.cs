using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using orneklerlelr.Dto;
using orneklerlelr.Models;

namespace orneklerlelr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly DataContext _context;

        public ProductsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await  _context
            .Products
            .Select(x => ProductToDto(x))         
            .ToListAsync();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var product = await _context
                .Products   
                .FindAsync(id);
            return Ok(ProductToDto(product));
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product entity)
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = entity.ProductId },ProductToDto( entity));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product entity)
        { 
        if(id! == entity.ProductId)
            {
                return BadRequest();
            }
        var product = await _context.Products.FindAsync(id);
            if (product == null)
            {


                return NotFound(); 
            }
            product.Name = entity.Name;
            product.Price = entity.Price;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if(products == null)
            {
                return NotFound();
            }
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private static ProductDto ProductToDto(Product p)
        {
            return new ProductDto()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive,
            };
        }
    }
}
