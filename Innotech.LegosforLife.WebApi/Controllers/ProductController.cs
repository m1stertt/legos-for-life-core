using System.Collections.Generic;
using System.IO;
using InnoTech.LegosForLife.Core.IServices;
using InnoTech.LegosForLife.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoTech.LegosForLife.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController: ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new InvalidDataException("ProductService Cannot Be Null");
        }
        
        [HttpPost]  
        public ActionResult<Product> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("A product is required to create a product in the repository");
            }

            return Ok(_productService.Create(product));
        }
        
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return _productService.GetProducts();
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to find by id.");
            }
            return Ok(_productService.GetProductById(id));
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult<Product> DeleteById(int id)
        {
            if (id == 0)
            {
                return BadRequest("An ID is required to delete by id.");
            }
            return Ok(_productService.DeleteById(id));
        }
        

        [HttpPut("{id}")]  
        public ActionResult<Product> Update(int id, [FromBody] Product product)
        {
            if (id < 1 || id != product.Id)
            {
                return BadRequest("Correct id is needed to update a product.");
            }

            
            return Ok(_productService.Update(product));
        }
    }
}