using E_Commerce_API.Data;
using E_Commerce_API.Logging;
using E_Commerce_API.Models;
using E_Commerce_API.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        //private readonly ILogger<ProductsAPIController> _logger;
        private readonly ILogging _logger;
        public ProductsAPIController(ILogging logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetProducts() 
        {
            _logger.Log("getting all products","");
            return Ok(ProductsStore.productsList);
        }
       
        [HttpGet("{id:int}",Name ="GetProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDTO> GetProduct(int id) 
        {
            if (id == 0) 
            {
                _logger.Log("Get product error with Id" + id,"error");
                return BadRequest();
            }
            var product = ProductsStore.productsList.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductDTO> createProduct([FromBody]ProductDTO productDTO)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            if(ProductsStore.productsList.FirstOrDefault(u=>u.ProductName.ToLower() == productDTO.ProductName.ToLower())!=null) 
            {
                ModelState.AddModelError("customError", "Product already exists");
                return BadRequest(ModelState);
            }
            if (productDTO == null) 
            {
                return BadRequest(productDTO);
            }
            if (productDTO.Id > 0) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            productDTO.Id = ProductsStore.productsList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            ProductsStore.productsList.Add(productDTO);

            return CreatedAtRoute("GetProduct",new {id = productDTO.Id},productDTO);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}",Name = "DeleteProduct")]
        
        public IActionResult DeleteProduct(int id) 
        {
            if (id == 0) 
            {
                return BadRequest();
            }

            var product = ProductsStore.productsList.FirstOrDefault(u => u.Id == id);

            if (product == null) 
            {
                return NotFound();
            }

            ProductsStore.productsList.Remove(product);
            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id:int}", Name = "UpdateProduct")]

        public IActionResult UpdateProduct(int id, [FromBody]ProductDTO productDTO) 
        {
            if (productDTO == null || id != productDTO.Id) 
            {
                return BadRequest();
            }

            var product = ProductsStore.productsList.FirstOrDefault(u => u.Id == id);

            if (product == null) 
            {
                return NotFound();
            }

            product.ProductName = productDTO.ProductName;
            product.Category = productDTO.Category;
            product.Price = productDTO.Price;

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<ProductDTO> patchDTO) 
        {
            if (patchDTO == null || id == 0) 
            {
                return BadRequest();
            }

            var product = ProductsStore.productsList.FirstOrDefault(u => u.Id == id);

            if (product == null) 
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(product, ModelState);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }



    }
}
