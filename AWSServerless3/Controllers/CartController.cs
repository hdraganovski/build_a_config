namespace AWSServerless3.Controllers
{
    using AWSServerless3.Models;
    using AWSServerless3.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        /// <summary>
        /// GET api/cart/user/{userId}
        /// Get cart for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public async Task<Cart> GetOrCreateCartForUser([FromRoute] string userId)
        {
            return await cartService.GetOrCreateCartForUser(userId);
        }

        /// <summary>
        /// GET api/cart/buy/{cartId}/{userId}
        /// Buy everything from a cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("buy/{cartId}/{userId}")]
        public async Task<Cart> Buy([FromRoute] string cartId, [FromRoute] string userId)
        {
            return await cartService.Buy(cartId, userId);
        }

        /// <summary>
        /// GET api/cart/clear/{cartId}
        /// Remove everything from a cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpGet("clear/{cartId}")]
        public async Task<Cart> Clear([FromRoute] string cartId)
        {
            return await cartService.Clear(cartId);
        }

        /// <summary>
        /// POST api/cart/cartItem/{cartId}
        /// Add item to cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        [HttpPost("cartItem/{cartId}")]
        public async Task<Cart> AddCartItem([FromRoute] string cartId, [FromBody] CartItem cartItem)
        {
            return await cartService.AddCartItem(cartId, cartItem);
        }

        /// <summary>
        /// DELETE api/cart/cartItem/{cartId}/{productId}
        /// Remove an item from cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("cartItem/{cartId}/{productId}")]
        public async Task<Cart> RemoveCartItem([FromRoute] string cartId, [FromRoute]  string productId)
        {
            return await cartService.RemoveCartItem(cartId, productId);
        }

        /// <summary>
        /// GET api/cart/increment/{cartId}/{productId}
        /// Increment products
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpGet("increment/{cartId}/{productId}")]
        public async Task<Cart> IncrementProduct([FromRoute] string productId, [FromRoute] string cartId)
        {
            return await cartService.Increment(cartId, productId);
        }

        /// <summary>
        /// GET api/cart/decrement/{cartId}/{productId}
        /// Decrement products
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpGet("decrement/{cartId}/{productId}")]
        public async Task<Cart> DecrementProduct([FromRoute] string productId, [FromRoute] string cartId)
        {
            return await cartService.Decrement(cartId, productId);
        }

        /// <summary>
        /// GET api/cart/list/{userId}
        /// Get all carts for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("list/{userId}")]
        public async Task<IEnumerable<Cart>> ListCarts([FromRoute] string userId)
        {
            return await cartService.ListCartsForUser(userId);
        }
    }
}