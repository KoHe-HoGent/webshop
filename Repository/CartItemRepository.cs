using Microsoft.EntityFrameworkCore;
using webshop_2.Data;
using webshop_2.Models;
using webshop_2.Models.Interfaces;

namespace Webshop3.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly ApplicationDbContext _context;
        public CartItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(CartItem cartItem)
        {
            _context.Add(cartItem);
            return Save();
        }

        public async Task<bool> Delete(int id)
        {
            var cartitem = await GetById(id);
            cartitem.IsDeleted = true;
            _context.Update(cartitem);
            return Save();
        }

        public async Task<IEnumerable<CartItem>> GetByCartId(int cartId)
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem> GetById(int cartItemId)
        {
            return await _context.CartItems
                .Include(ci => ci.ShopItem)
                .Include(ci => ci.Cart)
                .FirstAsync(c => c.Id == cartItemId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(CartItem cartItem)
        {
            _context.Update(cartItem);
            return Save();
        }
    }
}
