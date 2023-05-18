using E_Commerce_API.Data;
using E_Commerce_API.Models;
using E_Commerce_API.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsAPIController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProductDTO> GetProducts() 
        {
            return ProductsStore.productsList;
        }
       
        [HttpGet("{id:int}")]
        public ProductDTO GetProduct(int id) 
        {
            return ProductsStore.productsList.FirstOrDefault(u => u.Id == id);
        }
    }
}
