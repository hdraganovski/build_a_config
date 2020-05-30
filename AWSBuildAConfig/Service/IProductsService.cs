namespace AWSBuildAConfig.Service
{
    using AWSBuildAConfig.Entity;
    using System.Collections.Generic;

    public interface IProductsService
    {
        List<Product> GetAllProducts();
    }
}
