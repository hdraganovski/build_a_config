namespace AWSBuildAConfig.Repository.Impl
{
    using AWSBuildAConfig.Entity;
    using System;
    using System.Collections.Generic;

    public class ProductsRepository : IProductsRepository
    {
        public List<Product> GetAllProducts()
        {
            var list = new List<Product>();
            
            for(int i=0; i<10; i++)
            {
                list.Add(new Product
                {
                    Uid = Guid.NewGuid().ToString(),
                    Category = "Category",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In vel congue enim. Sed auctor, risus id vestibulum lobortis, diam ante blandit magna, eget consequat ligula tortor vitae metus. Etiam iaculis dui eget est accumsan molestie. Cras semper est risus, id laoreet metus viverra non. Aliquam sit amet molestie tellus. Sed ut imperdiet odio.",
                    ImageUrls = new List<string> { "https://d1a68gwbwfmqto.cloudfront.net/img/products/full/gtx1650supergamingx.jpg" },
                    Name = "Name",
                    Price = 13690m + i * 10m
                });
            }

            return list;
        }
    }
}
