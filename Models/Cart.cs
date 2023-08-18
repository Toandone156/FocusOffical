namespace FocusOffical.Models
{
    public class Cart
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
    }
}
