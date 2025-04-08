using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProviderServiceService
    {
        Task<List<ProviderService>> GetAll();
        Task<ProviderService> GetById(int id);
        Task Create(ProviderService model);
        Task Update(ProviderService model);
        Task Delete(int id);
    }
}