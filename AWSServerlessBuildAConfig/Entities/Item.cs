namespace AWSServerlessBuildAConfig.Entities
{
    using System;

    public class Item
    {
        public string Uid { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
