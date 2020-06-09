namespace AWSServerless3.Models
{
    using Amazon.DynamoDBv2.DataModel;

    [DynamoDBTable("Category")]
    public class Category
    {
        [DynamoDBHashKey]
        public string Id { get; set; }


        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public string ImageUrl { get; set; }
    }
}
