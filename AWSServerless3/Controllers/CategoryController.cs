namespace AWSServerless3.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AWSServerless3.Models;
    using AWSServerless3.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public CategoryController(ICategoryService categoryService,
                                  IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }

        /// <summary>
        /// GET api/category 
        /// Lists all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Category>> Get()
        {
            return await categoryService.ListAllCategories();
        }

        /// <summary>
        /// GET api/category/{name}
        /// Lists all products for a category
        /// TODO Paging and sorting and filtering
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IEnumerable<Product>> GetProductsForCategory(string name)
        {
            return await productService.GetProductsForCategory(name);
        }

        [HttpPost("add")]
        public async Task<Category> AddCategory(Category category)
        {
            return await categoryService.AddCategory(category);
        }
    }
}