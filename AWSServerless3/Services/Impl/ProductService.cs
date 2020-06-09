namespace AWSServerless3.Services.Impl
{
    using Amazon.DynamoDBv2.DataModel;
    using AWSServerless3.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductService : IProductService
    {
        private readonly IDynamoDBContext context;

        public ProductService(IDynamoDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsForCategory(string name)
        {
            var products = await context.ScanAsync<Product>(new List<ScanCondition>
            {
                new ScanCondition("Category",
                                  Amazon.DynamoDBv2.DocumentModel.ScanOperator.Contains,
                                  new string[] { name })
            }).GetRemainingAsync();

            return products;
        }

        public async Task<Product> GetProductById(string id)
        {
            var product = await context.LoadAsync<Product>(id);
            return product;
        }

        public async Task<List<Product>> SearchProducts(string searchTerm)
        {
            var products = await context.ScanAsync<Product>(new List<ScanCondition>
            {
                new ScanCondition("Name", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Contains, new string[] { searchTerm })
            }).GetRemainingAsync();

            return products;
        }

        public async Task<List<Product>> GetAllByMannufacturer(string mannufacturer)
        {
            var products = await context.ScanAsync<Product>(new List<ScanCondition>
            {
                new ScanCondition("Mannufacturer", Amazon.DynamoDBv2.DocumentModel.ScanOperator.Contains, new string[] { mannufacturer })
            }).GetRemainingAsync();

            return products;
        }

        public async Task<Product> AddProduct(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            await context.SaveAsync(product);
            return product;
        }
    }
}
