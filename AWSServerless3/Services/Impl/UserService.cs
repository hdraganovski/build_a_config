using Amazon.DynamoDBv2.DataModel;
using AWSServerless3.Models;
using AWSServerless3.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSServerless3.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IDynamoDBContext context;

        public UserService(IDynamoDBContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await context.LoadAsync<User>(id);
            return user;
        }

        public async Task<User> LogIn(LogInRequest logInRequest)
        {
            var user = (await context.ScanAsync<User>(new List<ScanCondition>
            {
                new ScanCondition("Email",
                                  Amazon.DynamoDBv2.DocumentModel.ScanOperator.Contains,
                                  new string[] { logInRequest.Email })
            }).GetRemainingAsync()).FirstOrDefault();

            if (user == null || !user.Password.Equals(logInRequest.Password))
            {
                return null;
            }

            return user;
        }

        public async Task<User> Register(RegisterRequest registerRequest)
        {
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerRequest.Email,
                FullName = registerRequest.FullName,
                Password = registerRequest.Password
            };

            await context.SaveAsync(user);

            return user;
        }
    }
}
