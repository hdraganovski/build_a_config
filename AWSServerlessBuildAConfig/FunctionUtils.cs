namespace AWSServerlessBuildAConfig
{
    using Amazon.Lambda.APIGatewayEvents;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net;

    class FunctionUtils
    {
        #region Responses
        
        public static APIGatewayProxyResponse GetOkResponse<T>(T item)
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
    }
}
