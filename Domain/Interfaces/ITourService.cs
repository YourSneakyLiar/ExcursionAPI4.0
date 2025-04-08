using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITourService
    {
        Task<List<Tour>> GetAll();
        Task<Tour> GetById(int id);
        Task Create(Tour model);
        Task Update(Tour model);
        Task Delete(int id);
    }
}