namespace AWSServerless3.Services.Impl
{
    using Amazon.DynamoDBv2.DataModel;
    using Amazon.DynamoDBv2.DocumentModel;
    using AWSServerless3.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CartService : ICartService
    {
        private readonly IDynamoDBContext context;

        public CartService(IDynamoDBContext context)
        {
            this.context = context;
        }

        public async Task<Cart> GetOrCreateCartForUser(string userId)
        {
            var carts = await context.ScanAsync<Cart>(new List<ScanCondition>
            {
                new ScanCondition("UserId", ScanOperator.Equal, new string[] { userId }),
                new ScanCondition("CartStatus", ScanOperator.Equal, new string[] { "OPENED" })
            }).GetRemainingAsync();

            if (carts.Count == 0)
            {
                var cart = new Cart
                {
                    Id = Guid.NewGuid().ToString(),
                    CartItems = new List<CartItem>(),
                    CartStatus = "OPENED",
                    UserId = userId
                };

                await context.SaveAsync(cart);
                return cart;
            }
            else
            {
                return carts[0];
            }
        }

        public async Task<Cart> AddCartItem(string cartId, CartItem cartItem)
        {
            var cart = await context.LoadAsync<Cart>(cartId);
            var existingCartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == cartItem.ProductId);
            if (existingCartItem == null)
            {
                cart.CartItems.Add(cartItem);
            }
            else
            {
                existingCartItem.Count++;
            }
            await context.SaveAsync(cart);
            return cart;
        }

        public async Task<Cart> RemoveCartItem(string cartId, string productId)
        {
            var cart = await context.LoadAsync<Cart>(cartId);
            cart.CartItems.RemoveAll(item => item.ProductId == productId);
            await context.SaveAsync(cart);
            return cart;
        }

        public async Task<Cart> Clear(string cartId)
        {
            var cart = await context.LoadAsync<Cart>(cartId);
            cart.CartItems.Clear();
            await context.SaveAsync(cart);
            return cart;
        }

        public async Task<Cart> Buy(string cartId, string userId)
        {
            var cart = await context.LoadAsync<Cart>(cartId);
            cart.CartStatus = "BOUGHT";
            await context.SaveAsync(cart);
            var newCart = new Cart
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                CartItems = new List<CartItem>(),
                CartStatus = "OPENED"
            };
            await context.SaveAsync(newCart);
            return newCart;
        }

        public async Task<Cart> Increment(string cartId, string productId)
        {
            var cart = await context.LoadAsync<Cart>(cartId);
            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem == null)
            {
                return cart;
            }

            cartItem.Count++;

            await context.SaveAsync(cart);
            return cart;
        }

        public async Task<Cart> Decrement(string cartId, string productId)
        {
            var cart = await context.LoadAsync<Cart>(cartId);
            var cartItem = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);

            if (cartItem == null)
            {
                return cart;
            }

            if(cartItem.Count <= 1)
            {
                cart.CartItems.Remove(cartItem);
            }
            else
            {
                cartItem.Count--;
            }

            await context.SaveAsync(cart);
            return cart;
        }

        public async Task<IEnumerable<Cart>> ListCartsForUser(string userId)
        {
            var carts = await context.ScanAsync<Cart>(new List<ScanCondition>
            {
                new ScanCondition("UserId", ScanOperator.Equal, new string[] { userId })
            }).GetRemainingAsync();
            return carts;
        }
    }
}
