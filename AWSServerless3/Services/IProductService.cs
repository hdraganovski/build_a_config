namespace AWSServerless3.Services
{
    using AWSServerless3.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        Task<List<Product>> GetAllByMannufacturer(string mannufacturer);
        Task<Product> GetProductById(string id);
        Task<IEnumerable<Product>> GetProductsForCategory(string name);
        Task<List<Product>> SearchProducts(string searchTerm);
        Task<Product> AddProduct(Product product);
    }
}
