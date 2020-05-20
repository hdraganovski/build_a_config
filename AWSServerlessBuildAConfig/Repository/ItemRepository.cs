using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWSServerlessBuildAConfig.Repository
{
    public class ItemRepository
    {
        private const string TABLENAME_ENVIRONMENT_VARIABLE_LOOKUP = "ItemTable";

        private IDynamoDBContext DDBContext;

        public ItemRepository(IDynamoDBContext DDBContext)
        {
            this.DDBContext = DDBContext;
        }
    }
}
