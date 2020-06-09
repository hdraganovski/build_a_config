namespace AWSServerless3.Models
{
    using Amazon.DynamoDBv2.DataModel;

    [DynamoDBTable("User")]
    public class User
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string FullName { get; set; }

        [DynamoDBProperty]
        public string Email { get; set; }

        [DynamoDBProperty]
        public string Password { get; set; }
    }
}
