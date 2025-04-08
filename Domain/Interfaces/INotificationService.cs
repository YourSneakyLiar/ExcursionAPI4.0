using Domain.Models;

namespace Domain.Interfaces
{
    public interface INotificationService
    {
        Task<List<Notification>> GetAll();
        Task<Notification> GetById(int id);
        Task Create(Notification model);
        Task Update(Notification model);
        Task Delete(int id);
    }
}