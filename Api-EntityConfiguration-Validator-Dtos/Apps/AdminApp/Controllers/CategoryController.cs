using Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Dtos.CategoryDto;
using Api_EntityConfiguration_Validator_Dtos.DAL;
using Api_EntityConfiguration_Validator_Dtos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api_EntityConfiguration_Validator_Dtos.Apps.AdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ShopAppDbContext _context) : ControllerBase
    {
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = _context.Categories.Select(c => new CategoryReturnDto()
            {
                Id = c.Id,
                Name = c.Name,
                CreatedDate = c.CreatedDate,
                UpdatedDate = c.UpdatedDate,
                ImageURL = c.ImageURL,
            })
                .ToList();
            if (categories == null) return NotFound();
            return Ok(categories);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            var existCategory = _context.Categories.FirstOrDefault(c => c.Id == id && !c.IsDelete);
            if (existCategory == null) return BadRequest();
            CategoryReturnDto categoryReturnDto = new()
            {
                Id = existCategory.Id,
                Name = existCategory.Name,
                ImageURL = "http://localhost:5159/img//" + existCategory.ImageURL,
            };

            return Ok(categoryReturnDto);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            var isExist = await _context.Categories.AnyAsync(e => e.IsDelete && e.Name.ToLower() == categoryCreateDto.Name.ToLower());
            if (isExist) return StatusCode(409);

            Category category = new();
            if (categoryCreateDto.ImgaeURLUpload == null) return BadRequest();

            if (!categoryCreateDto.ImgaeURLUpload.ContentType.Contains("image/")) return BadRequest();

            if (categoryCreateDto.ImgaeURLUpload.Length / 1024 > 500) return BadRequest();

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryCreateDto.ImgaeURLUpload.FileName);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

            using FileStream fileStream = new(path, FileMode.Create);

            await categoryCreateDto.ImgaeURLUpload.CopyToAsync(fileStream);

            category.Name = categoryCreateDto.Name;
            category.ImageURL = fileName;
            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(201);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id == null) return BadRequest();
            var existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.IsDelete);
            if (existCategory == null) return BadRequest();

            if (categoryUpdateDto.ImgaeURLUpload == null) return BadRequest();

            if (!categoryUpdateDto.ImgaeURLUpload.ContentType.Contains("image/")) return BadRequest();

            if (categoryUpdateDto.ImgaeURLUpload.Length / 1024 > 500) return BadRequest();

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryUpdateDto.ImgaeURLUpload.FileName);

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

            using FileStream fileStream = new(path, FileMode.Create);

            await categoryUpdateDto.ImgaeURLUpload.CopyToAsync(fileStream);

            existCategory.Name = categoryUpdateDto.Name;
            existCategory.ImageURL = fileName;
            existCategory.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var existCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.IsDelete);
            if (existCategory == null) return BadRequest();
            _context.Categories.Remove(existCategory);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
