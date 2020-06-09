namespace AWSServerless3.Controllers
{
    using AWSServerless3.Models;
    using AWSServerless3.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// GET api/product/search/{searchTerm}
        /// Search for products by name
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        [HttpGet("search/{searchTerm}")]
        public async Task<List<Product>> Search(string searchTerm)
        {
            return await productService.SearchProducts(searchTerm);
        }

        /// <summary>
        /// GET api/product/manufacturer/{manufacturer}
        /// Return products from manufacturer.
        /// </summary>
        /// <param name="mannufacturer"></param>
        /// <returns></returns>
        [HttpGet("manufacturer/{manufacturer}")]
        public async Task<List<Product>> GetAllByManufacturer(string mannufacturer)
        {
            return await productService.GetAllByMannufacturer(mannufacturer);
        }

        /// <summary>
        /// GET api/product/{id}
        /// Returns a product with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Product> GetById(string id)
        {
            return await productService.GetProductById(id);
        }

        [HttpPost]
        public async Task<Product> AddProduct(Product product)
        {
            return await productService.AddProduct(product);
        }
    }
}