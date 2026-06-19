using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
