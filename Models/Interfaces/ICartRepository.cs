namespace webshop_2.Models.Interfaces
{
    public interface ICartRepository
    {
        bool Add(Cart cart);
        bool Update(Cart cart);
        bool Delete(Cart cart);
        void Clear(Cart cart);
        bool Save();
        Task<Cart> GetById(int cartId);
        Task<IEnumerable<Cart>> GetAll();
    }
}
