using Domain.Models;

namespace Domain.Interfaces
{
    public interface ITourLoadStatisticService
    {
        Task<List<TourLoadStatistic>> GetAll();
        Task<TourLoadStatistic> GetById(int id);
        Task Create(TourLoadStatistic model);
        Task Update(TourLoadStatistic model);
        Task Delete(int id);
    }
}