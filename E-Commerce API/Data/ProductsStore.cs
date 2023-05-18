using E_Commerce_API.Models.Dto;

namespace E_Commerce_API.Data
{
    public class ProductsStore
    {
        public static List<ProductDTO> productsList = new List<ProductDTO>
        {
             new ProductDTO { Id = 1, ProductName = "Samsung", Category="Mobile", Price=25000},
             new ProductDTO { Id = 2,ProductName = "Apple IPhone", Category = "Mobile", Price = 85000}
        };
    }
}
