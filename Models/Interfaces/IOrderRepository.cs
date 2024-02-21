namespace webshop_2.Models.Interfaces
{
    public interface IOrderRepository
    {
        bool Add(Order order);
        bool Update(Order order);
        Task<bool> Delete(int id);
        bool Save();
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetByAppUserId(string appuserId);
        Task<Order> GetById(int orderId);
    }
}
