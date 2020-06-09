namespace AWSServerless3.Services
{
    using AWSServerless3.Models;
    using AWSServerless3.Requests;
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task<User> GetUserById(string id);
        Task<User> LogIn(LogInRequest logInRequest);
        Task<User> Register(RegisterRequest registerRequest);
    }
}
