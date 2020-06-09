namespace AWSServerless3.Services
{
    using AWSServerless3.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Task<IEnumerable<Category>> ListAllCategories();
        Task<Category> AddCategory(Category category);
    }
}
