using Domain.Models;

namespace Domain.Interfaces
{
    public interface ISubscriptionService
    {
        Task<List<Subscription>> GetAll();
        Task<Subscription> GetById(int id);
        Task Create(Subscription model);
        Task Update(Subscription model);
        Task Delete(int id);
    }
}