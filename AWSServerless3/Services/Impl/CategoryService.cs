namespace AWSServerless3.Services.Impl
{
    using Amazon.DynamoDBv2.DataModel;
    using AWSServerless3.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService: ICategoryService
    {
        private readonly IDynamoDBContext context;

        public CategoryService(IDynamoDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> ListAllCategories()
        {
            var cat = await context.ScanAsync<Category>(new List<ScanCondition>()).GetRemainingAsync();
            return cat;
        }

        public async Task<Category> AddCategory(Category category)
        {
            category.Id = Guid.NewGuid().ToString();
            await context.SaveAsync<Category>(category);
            return category;
        }
    }
}
