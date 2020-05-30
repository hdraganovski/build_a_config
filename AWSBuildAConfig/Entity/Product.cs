using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSBuildAConfig.Entity
{
    public class Product
    {
        public string Uid { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public List<string> ImageUrls { get; set; }
    }
}
