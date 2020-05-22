namespace AWSServerlessBuildAConfig.Repositories
{
    using Amazon.DynamoDBv2.DataModel;
    using AWSServerlessBuildAConfig.Entities;

    class UserRepository : CrudRepository<User>
    {
        public static readonly string TableName = "UserTable";

        public UserRepository(IDynamoDBContext DDBContext) : base(DDBContext, TableName)
        { }
    }
}
