using FocusOffical.Models;

namespace FocusOffical.Services
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product? GetById(int id);
    }

    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>
        {
            new Product
            {
                ProductId = 1,
                Name = "Test T-Shirt",
                Description = "Test test",
                Price = 100_000,
                Category = "new-products",
                Image = "product-1.jpg"
            },
            new Product
            {
                ProductId = 2,
                Name = "Test T-Shirt 2",
                Description = "Test test",
                Price = 100_000,
                Category = "new-products",
                Image = "product-2.jpg"
            },
            new Product
            {
                ProductId = 3,
                Name = "Test T-Shirt 3",
                Description = "Test test",
                Price = 100_000,
                Category = "new-products",
                Image = "product-3.jpg"
            },
            new Product
            {
                ProductId = 4,
                Name = "Test T-Shirt 4",
                Description = "Test test",
                Price = 100_000,
                Category = "new-products",
                Image = "product-4.jpg"
            },
        };

        public List<Product> GetAll()
        {
            return products;
        }

        public Product? GetById(int id)
        {
            return products.FirstOrDefault(p => p.ProductId == id);
        }
    }
}
