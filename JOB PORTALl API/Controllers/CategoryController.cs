using AutoMapper;
using Booking.Core.Dtos;
using Booking.Core.Entity;
using Booking.Core.Pero_Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JOB_PORTALl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {


        public readonly IGenaricRepo<Category> _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryController(IGenaricRepo<Category> genaricRepo , IMapper mapper)
        {
            _categoryRepo = genaricRepo;
            _mapper = mapper;
        }



        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }





        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }




        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto Category)
        {
            var newCategory = new CategoryDto
            {
                Name = Category.Name,
                Description = Category.Description
            };
            
            var newone = _mapper.Map<CategoryDto, Category>(newCategory);
            await _categoryRepo.AddAsync(newone);
            return Ok(new { message = "Category added successfully." });

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryRepo.DeleteAsync(id);
            return Ok(new { message = "Category deleted successfully." });
        }


    }
}
