namespace AWSServerless3.Services
{
    using AWSServerless3.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartService
    {
        Task<Cart> AddCartItem(string cartId, CartItem cartItem);
        Task<Cart> Buy(string cartId, string userId);
        Task<Cart> Clear(string cartId);
        Task<Cart> Decrement(string cartId, string productId);
        Task<Cart> GetOrCreateCartForUser(string userId);
        Task<Cart> Increment(string cartId, string productId);
        Task<IEnumerable<Cart>> ListCartsForUser(string userId);
        Task<Cart> RemoveCartItem(string cartId, string productId);
    }
}
