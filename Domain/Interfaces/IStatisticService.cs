using Domain.Interfacess;
using Domain.Models;

namespace Domain.Interfacess
{
    public interface IStatisticService
    {
        Task<List<Statistic>> GetAll();
        Task<Statistic> GetById(int id);
        Task Create(Statistic model);
        Task Update(Statistic model);
        Task Delete(int id);
    }
}
