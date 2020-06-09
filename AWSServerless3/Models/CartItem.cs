namespace AWSServerless3.Models
{
    public class CartItem
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Price { get; set; }

        public decimal Count{ get; set; }
    }
}
