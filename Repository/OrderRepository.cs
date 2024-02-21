using Microsoft.EntityFrameworkCore;
using webshop_2.Data;
using webshop_2.Models;
using webshop_2.Models.Interfaces;

namespace Webshop3.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Order Order)
        {
            _context.Add(Order);
            return Save();
        }

        public async Task<bool> Delete(int id)
        {
            var order = await GetById(id);
            order.IsDeleted = true;
            Update(order);
            return Save();
        }

        public async Task<Order> GetById(int OrderId)
        {
            return await _context.Orders.Where(o => o.Id == OrderId)
                .Include(o => o.AppUser)
                .Include(o => o.Cart)
                .ThenInclude(c => c.CartItems)
                .ThenInclude(ci => ci.ShopItem)
                .FirstAsync();
        }

        public async Task<IEnumerable<Order>> GetByAppUserId(string AppUserId)
        {
            return await _context.Orders.Where(o => o.AppUserId == AppUserId)
                .Include(o => o.AppUser).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Order Order)
        {
            _context.Update(Order);
            return Save();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
