namespace AWSBuildAConfig.Service.Impl
{
    using AWSBuildAConfig.Entity;
    using AWSBuildAConfig.Repository;
    using System.Collections.Generic;

    public class ProductService : IProductsService
    {
        private readonly IProductsRepository ProductsRepository;

        public ProductService(IProductsRepository productsRepository)
        {
            ProductsRepository = productsRepository;
        }

        public List<Product> GetAllProducts()
        {
            return ProductsRepository.GetAllProducts();
        }
    }
}
