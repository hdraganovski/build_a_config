namespace AWSServerless3.Models
{
    using Amazon.DynamoDBv2.DataModel;
    using System.Collections.Generic;

    [DynamoDBTable("Cart")]
    public class Cart
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string UserId { get; set; }

        [DynamoDBProperty]
        public List<CartItem> CartItems { get; set; }

        [DynamoDBProperty]
        public string CartStatus { get; set; }
    }
}
