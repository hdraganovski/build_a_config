using System;
using System.Collections.Generic;

namespace AWSServerlessBuildAConfig.Entities
{
    public class User
    {
        public string Uid { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public List<Configuration> Configurations { get; set; }
    }
}
