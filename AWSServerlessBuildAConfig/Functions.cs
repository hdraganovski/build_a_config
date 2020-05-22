using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

using Newtonsoft.Json;
using AWSServerlessBuildAConfig.Repositories;
using AWSServerlessBuildAConfig.Services;
using AWSServerlessBuildAConfig.Entities;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSServerlessBuildAConfig
{
    public class Functions
    {
        public const string ID_QUERY_STRING_NAME = "Uid";

        IDynamoDBContext DDBContext { get; set; }

        private ItemRepository itemRepository { get; set; }

        private UserRepository userRepository { get; set; }

        private ItemService itemService { get; set; }

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
            // Check to see if a table name was passed in through environment variables and if so 
            // add the table mapping.
            var tableName = System.Environment.GetEnvironmentVariable(TABLENAME_ENVIRONMENT_VARIABLE_LOOKUP);
            if (!string.IsNullOrEmpty(tableName))
            {
                AWSConfigsDynamoDB.Context.TypeMappings[typeof(Blog)] = new Amazon.Util.TypeMapping(typeof(Blog), tableName);
            }

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(Item)] = new Amazon.Util.TypeMapping(typeof(Item), ItemRepository.TableName);
            AWSConfigsDynamoDB.Context.TypeMappings[typeof(User)] = new Amazon.Util.TypeMapping(typeof(User), UserRepository.TableName);

            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            this.DDBContext = new DynamoDBContext(new AmazonDynamoDBClient(), config);

            itemRepository = new ItemRepository(DDBContext);
            userRepository = new UserRepository(DDBContext);

            itemService = new ItemService(itemRepository);
        }

        /// <summary>
        /// Constructor used for testing passing in a preconfigured DynamoDB client.
        /// </summary>
        /// <param name="ddbClient"></param>
        /// <param name="tableName"></param>
        public Functions(IAmazonDynamoDB ddbClient, string tableName)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                AWSConfigsDynamoDB.Context.TypeMappings[typeof(Blog)] = new Amazon.Util.TypeMapping(typeof(Blog), tableName);
            }

            AWSConfigsDynamoDB.Context.TypeMappings[typeof(Item)] = new Amazon.Util.TypeMapping(typeof(Item), ItemRepository.TableName);
            AWSConfigsDynamoDB.Context.TypeMappings[typeof(User)] = new Amazon.Util.TypeMapping(typeof(User), UserRepository.TableName);

            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            this.DDBContext = new DynamoDBContext(ddbClient, config);

            itemRepository = new ItemRepository(DDBContext);
            userRepository = new UserRepository(DDBContext);

            itemService = new ItemService(itemRepository);
        }

        #region Utils
        
        public static Guid? GetRequestedUid(APIGatewayProxyRequest request)
        {
            try
            {
                string id = null;
                if (request.PathParameters != null && request.PathParameters.ContainsKey(ID_QUERY_STRING_NAME))
                    id = request.PathParameters[ID_QUERY_STRING_NAME];
                else if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey(ID_QUERY_STRING_NAME))
                    id = request.QueryStringParameters[ID_QUERY_STRING_NAME];

                if(id == null)
                {
                    return Guid.Empty;
                }

                return Guid.Parse(id);
            }
            catch
            {
                return null;
            }
        }
        
        #endregion
        
        #region Responses

        public static APIGatewayProxyResponse GetOkResponse<T> (T item)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(item),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        public static APIGatewayProxyResponse GetBlankOkResponse()
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public static APIGatewayProxyResponse GetNotFoundResponse()
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }

        public static APIGatewayProxyResponse GetBadRequestResonse(string message)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = message
            };
        }

        #endregion

        #region Item Endpoints

        public async Task<APIGatewayProxyResponse> GetItemsAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.LogLine("Getting items");
            var page = await itemService.GetAll();
            context.Logger.LogLine($"Found {page.Count} blogs");

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(page),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };

            return response;
        }

        public async Task<APIGatewayProxyResponse> GetItemAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Guid? itemUid = GetRequestedUid(request);
            
            if (!itemUid.HasValue)
            {
                return GetBadRequestResonse($"Missing required parameter {ID_QUERY_STRING_NAME}");
            }

            context.Logger.LogLine($"Getting item {itemUid}");
            var item = await itemService.Get(itemUid.Value.ToString());
            context.Logger.LogLine($"Found item: {item != null}");

            if (item == null)
            {
                return GetNotFoundResponse();
            }

            return GetOkResponse(item);
        }

        public async Task<APIGatewayProxyResponse> SaveItemAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            var item = JsonConvert.DeserializeObject<Item>(request?.Body);

            context.Logger.LogLine($"Saving blog with id {item.Uid}");
            var id = await itemService.Save(item);

            return GetOkResponse(id);
        }

        public async Task<APIGatewayProxyResponse> DeleteItemAsync(APIGatewayProxyRequest request, ILambdaContext context)
        {
            Guid? itemUid = GetRequestedUid(request);
            
            if (!itemUid.HasValue)
            {
                return GetBadRequestResonse($"Missing required parameter {ID_QUERY_STRING_NAME}");
            }

            context.Logger.LogLine($"Deleting item with id {itemUid}");

            await itemService.Delete(itemUid.Value.ToString());

            return GetBlankOkResponse();
        }

        #endregion
    }
}
