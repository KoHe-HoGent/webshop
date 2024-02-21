namespace webshop_2.Models.Interfaces
{
    public interface ICartItemRepository
    {
        bool Add(CartItem cartItem);
        bool Update(CartItem cartItem);
        Task<bool> Delete(int id);
        bool Save();
        Task<IEnumerable<CartItem>> GetByCartId(int cartId);
        Task<CartItem> GetById(int cartItemId);
    }
}
