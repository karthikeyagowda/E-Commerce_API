namespace E_Commerce_API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName{ get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
