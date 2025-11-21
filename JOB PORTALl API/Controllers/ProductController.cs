using AutoMapper;
using Booking.Core.Dtos;
using Booking.Core.Pero_Contract;
using Booking.Core.Rpo.Contract;
using Booking.Repository.Repo;
using Job.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace JOB_PORTALl_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductRepo _productRepo;
        private readonly ISavedProducts _savedRepo;
        private readonly IMapper _mapper;

        public ProductController( IProductRepo productRepo ,ISavedProducts SavedRepo , IMapper mapper)
        {
            _productRepo = productRepo;
            _savedRepo = SavedRepo;
            _mapper = mapper;
        }



        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductToreturnDto Product )
        {

            await _productRepo.AddProduct(Product , Product.UserId);
            return Ok(new { message = "Product added successfully." });

        }




        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetProducts(int Category)
        {

            var products = await _productRepo.GetAllAsync(Category);
            return Ok(products);

        }





        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }





        [HttpPost("SaveProduct")]
        public async Task<IActionResult> SaveProductAsync( int productid ,[FromBody] string user)
        {
            try
            {
                await _savedRepo.SaveProduct(user , productid );
                return Ok(new { message = "product saved successfully." });
            }
            catch ( ArgumentException ex )
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while saving the post.", details = ex.Message });
            }
        }





        [HttpPost]
        [Route("GetAllSavedProductsByUserId")]
        public async Task<IActionResult> GetAllSavedProductsByUserIdAsync( string user)
        {
            try
            {
                var savedProducts = await _savedRepo.GetAllSavedProductsByUserIdAsync(user);
                return Ok(savedProducts);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving saved products.", details = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _savedRepo.DeleteSavedProduct(id);
            return Ok(new { message = "Saved product deleted successfully." });
        }



        [HttpDelete("Delete from Category/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productRepo.DeleteAsync(id);
            return Ok(new { message = "Product deleted successfully." });
        }





        [HttpPost("UpdateProduct/{id}")]
        public async Task<IActionResult> UpdateProduct (int id ,UpdateProductReq req)
        {
            var UpdatedProduct = await _productRepo.UpdateProduct(id , req);
            return Ok(UpdatedProduct);
        }
    }
}
