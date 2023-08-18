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
                Name = "Blooming",
                Description = "You are rose of the garden",
                Price = 239_000,
                Category = "blooming",
                Image = "product1.png"
            },
            new Product
            {
                ProductId = 2,
                Name = "Simple",
                Description = "If you love life. Life will love you back",
                Price = 239_000,
                Category = "simple",
                Image = "product2.png"
            },
            new Product
            {
                ProductId = 3,
                Name = "Focus Simple",
                Description = "If you love life. Life will love you back",
                Price = 239_000,
                Category = "simple",
                Image = "product3.png"
            },
            new Product
            {
                ProductId = 4,
                Name = "Focus Blooming",
                Description = "You are rose of the garden",
                Price = 239_000,
                Category = "blooming",
                Image = "product4.png"
            },
            new Product
            {
                ProductId = 5,
                Name = "08 Simple",
                Description = "If you love life. Life will love you back",
                Price = 239_000,
                Category = "simple",
                Image = "product5.png"
            },
            new Product
            {
                ProductId = 6,
                Name = "Sunflower",
                Description = "“Beauty from within’ - “A Rose can never be a sunflower, and a sunflower can never be a rose. All flowers are beautiful in their own way, and that’s like women too.”\r\n",
                Price = 199_000,
                Category = "sunflower",
                Image = "product6.jpg"
            }
            //new Product
            //{
            //    ProductId = 7,
            //    Name = "Risk 2023",
            //    Description = "“Beauty from within’ - “A Rose can never be a sunflower, and a sunflower can never be a rose. All flowers are beautiful in their own way, and that’s like women too.”\r\n",
            //    Price = 239_000,
            //    Category = "",
            //    Image = "product7.jpg"
            //},
            //new Product
            //{
            //    ProductId = 8,
            //    Name = "Summer Focus",
            //    Description = "“Beauty from within’ - “A Rose can never be a sunflower, and a sunflower can never be a rose. All flowers are beautiful in their own way, and that’s like women too.”\r\n",
            //    Price = 239_000,
            //    Category = "",
            //    Image = "product8.jpg"
            //}
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
