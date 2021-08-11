using Products.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Data
{
    public class ProductContextSeed
    {
        public static async Task SeedAsync(ProductContext productContext)
        {
            if (!productContext.Products.Any())
            {
                productContext.Products.AddRange(GetPreconfiguredProducts());

                await productContext.SaveChangesAsync();
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>() { 
                new Product() {  Category = "Book", Description = "A sample book", Name = "Secret of Mountains", Price = 500, Summary = "Bla bla bla"},
                new Product() {  Category = "Vehicle", Description = "A sample vehicle", Name = "Hoeing Machine", Price = 1000, Summary = "Bla bla bla"}};
        }
    }
}
