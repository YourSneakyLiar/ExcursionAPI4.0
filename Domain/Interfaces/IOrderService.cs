using Domain.Models;

namespace Domain.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAll();
        Task<Order> GetById(int id);
        Task Create(Order model);
        Task Update(Order model);
        Task Delete(int id);
    }
}