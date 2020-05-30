namespace AWSBuildAConfig.Repository
{
    using AWSBuildAConfig.Entity;
    using System.Collections.Generic;

    public interface IProductsRepository
    {
        List<Product> GetAllProducts();
    }
}
