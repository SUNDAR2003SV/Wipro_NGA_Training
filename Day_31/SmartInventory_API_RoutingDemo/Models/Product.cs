namespace SmartInventory_API_RoutingDemo.Models
{
    public class Product
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; } = 0;
    }
}
