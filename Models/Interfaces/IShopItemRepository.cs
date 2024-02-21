namespace webshop_2.Models.Interfaces
{
    public interface IShopItemRepository
    {
        bool Add(ShopItem shopItem);
        bool Update(ShopItem shopItem);
        bool Delete(ShopItem shopItem);
        bool Save();
        Task<IEnumerable<ShopItem>> GetAll();
        Task<ShopItem> GetById(int shopItemId);
    }
}
