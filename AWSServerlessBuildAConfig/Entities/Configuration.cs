namespace AWSServerlessBuildAConfig.Entities
{
    using System;
    using System.Collections.Generic;

    public class Configuration
    {
        public Guid Uid { get; set; }

        public List<Guid> ItemUids { get; set; }

        public DateTime ClosedOn { get; set; }

        public bool Bought { get; set; }
    }
}
