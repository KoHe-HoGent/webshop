using Microsoft.EntityFrameworkCore;
using webshop_2.Data;
using webshop_2.Models;
using webshop_2.Models.Interfaces;

namespace Webshop3.Repository
{
    public class ShopItemRepository : IShopItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ShopItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(ShopItem shopItem)
        {
            _context.Add(shopItem);
            return Save();
        }

        public bool Delete(ShopItem shopItem)
        {
            shopItem.IsDeleted = true;
            _context.Update(shopItem);
            return Save();
        }

        public async Task<IEnumerable<ShopItem>> GetAll()
        {
            return await _context.ShopItems.ToListAsync();
        }

        public async Task<ShopItem> GetById(int shopItemId)
        {
            return await _context.ShopItems.FirstOrDefaultAsync(si => si.Id == shopItemId);
        }   

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(ShopItem shopItem)
        {
            _context.Update(shopItem);
            return Save();
        }
    }
}
