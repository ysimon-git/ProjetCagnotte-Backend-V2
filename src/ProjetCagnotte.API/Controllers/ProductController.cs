using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetCagnotte.API.Requests;
using ProjetCagnotte.Application.DTOs;
using ProjetCagnotte.Application.Interfaces;

namespace ProjetCagnotte.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    //https://localhost:7289/swagger/index.html
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products=await _productService.GetAllProductAsync();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product=await _productService.GetProductByIdAsync(id); 
            
            if (product == null) 
                return NotFound();

            
            return Ok(product);
        }


        //no create without authorization
        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductRequest request)
        {
            await using var stream =request.Image.OpenReadStream();

            var dto = new CreateProductDto
            {
                ProductName = request.ProductName,
                ProductDescription =request.ProductDescription,

                Price = request.Price,

                Image = new UploadedFileDto
                {
                    Content = stream,
                    FileName = request.Image.FileName,
                    ContentType = request.Image.ContentType
                }
            };

            var productId =await _productService.AddProduct(dto);

            return Ok(new
            {
                id = productId
            });
        }



        //no update without authorization
        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpPut("updateProduct/{id}")]
        public async Task<Boolean> UpdateProduct(int id,UpdateProductDto product)
        {

            return await _productService.UpdateProduct(id, product);

        }


        //no delete without authorization
        //[Authorize(Roles = "Admin")]
        [Authorize]
        [HttpDelete("deleteProduct/{id}")]
        public async Task<Boolean> DeleteProduct(int id)
        {
            return await _productService.DeleteProduct(id);
        }

    }
}
