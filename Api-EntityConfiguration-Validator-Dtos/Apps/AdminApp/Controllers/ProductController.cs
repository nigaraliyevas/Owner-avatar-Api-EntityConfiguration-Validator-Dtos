using Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.ProductDto;
using Api_EntityConfiguration_Validator_Dtos.DAL;
using Api_EntityConfiguration_Validator_Dtos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(ShopAppDbContext _context) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get(string? search, int page = 1)
        {
            var query = _context.Products
                .Where(i => !i.IsDelete);

            if (!string.IsNullOrEmpty(search))
                query = query
                    .Where(p => p.Name.ToLower()
                    .Contains(search.ToLower()));

            ProductListDto productListDto = new()
            {
                Page = page,
                TotalCount = query
                .Count(),

                Items = await query
                .Skip((page - 1) * 2)
                .Take(2)
                .Select(p => new ProductReturnDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CostPrice = p.CostPrice,
                    SalePrice = p.SalePrice,
                    UpdatedDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Category = new()
                    {
                        Name = p.Category.Name,
                        ProductCount = p.Category.Products.Count()
                    }
                })
                .ToListAsync()
            };
            return Ok(productListDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDelete);
            if (product == null) return NotFound();
            ProductReturnDto productReturnDto = new()
            {
                Id = product.Id,
                Name = product.Name,
                CostPrice = product.CostPrice,
                SalePrice = product.SalePrice,
                CreatedDate = product.CreatedDate,
                UpdatedDate = product.UpdatedDate,
                Category = new()
                {
                    Name = product.Category.Name,
                    ProductCount = _context.Products.Count()
                }
            };
            if (productReturnDto == null) return NotFound();
            return Ok(productReturnDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null) return NotFound();
            Product product = new()
            {
                Name = productCreateDto.Name,
                CostPrice = productCreateDto.CostPrice,
                SalePrice = productCreateDto.SalePrice,
                CategoryId = productCreateDto.CategoryId,
            };
            if (product == null) return BadRequest();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, ProductUpdateDto productUpdateDto)
        {
            var existProduct = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);

            if (existProduct == null) return NotFound();

            if (!await _context.Categories
                .AnyAsync(c => !c.IsDelete || c.Id == id)) return BadRequest();

            existProduct.Name = productUpdateDto.Name;
            existProduct.SalePrice = productUpdateDto.SalePrice;
            existProduct.CostPrice = productUpdateDto.CostPrice;

            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int? id, bool status)
        {
            var existProduct = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existProduct == null) return NotFound();
            existProduct.IsDelete = status;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var existProduct = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);
            if (existProduct == null) return NotFound();
            _context.Remove(existProduct);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
