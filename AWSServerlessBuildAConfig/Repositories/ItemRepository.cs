namespace AWSServerlessBuildAConfig.Repositories
{
    using Amazon.DynamoDBv2.DataModel;
    using AWSServerlessBuildAConfig.Entities;

    public class ItemRepository : CrudRepository<Item>
    {
        public static readonly string TableName = "ItemTable";

        public ItemRepository(IDynamoDBContext DDBContext) : base(DDBContext, TableName)
        { }
    }
}
