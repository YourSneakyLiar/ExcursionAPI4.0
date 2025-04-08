using Domain.Models;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User> GetByIdWithToken(int userId);
        Task<User> GetByEmailWithToken(string email);
        // Дополнительные методы для работы с пользователями
    }
}
