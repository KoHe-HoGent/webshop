using Microsoft.EntityFrameworkCore;
using webshop_2.Data;
using webshop_2.Models;
using webshop_2.Models.Interfaces;

namespace Webshop3.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Cart cart)
        {
            _context.Add(cart);
            return Save();
        }

        public void Clear(Cart cart)
        {
            cart.ClearCart();
            this.Save();
        }

        public bool Delete(Cart cart)
        {
            cart.IsDeleted = true;
            var result = this.Update(cart);
            if (result) return result;
            else return false;
        }

        //tijdelijk (carts enkel ophalen indien ingelogd + enkel carts van ingelogde user)
        public async Task<IEnumerable<Cart>> GetAll()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart> GetById(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.ShopItem)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Cart cart)
        {
            _context.Update(cart);
            return Save();
        }
    }
}
