namespace AWSServerless3.Controllers
{
    using System.Threading.Tasks;
    using AWSServerless3.Models;
    using AWSServerless3.Requests;
    using AWSServerless3.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// POST api/user/register
        /// User registration
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<User> Register([FromBody] RegisterRequest registerRequest)
        {
            return await userService.Register(registerRequest);
        }

        /// <summary>
        /// POST api/user/log-in
        /// User log in
        /// </summary>
        /// <param name="logInRequest"></param>
        /// <returns></returns>
        [HttpPost("log-in")]
        public async Task<User> LogIn([FromBody] LogInRequest logInRequest)
        {
            return await userService.LogIn(logInRequest);
        }
    }
}