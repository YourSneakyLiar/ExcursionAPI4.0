using Domain.Interfaces;
using Domain.Interfacess;
using Domain.Models;
using Domain.Wrapper;

namespace BusinessLogic.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public StatisticService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Получает всю статистику.
        /// </summary>
        public async Task<List<Statistic>> GetAll()
        {
            return await _repositoryWrapper.Statistic.FindAll();
        }

        /// <summary>
        /// Получает статистику по ID.
        /// </summary>
        public async Task<Statistic> GetById(int id)
        {
            var statistic = await _repositoryWrapper.Statistic
                .FindByCondition(x => x.StatisticId == id);

            return statistic.First();
        }

        /// <summary>
        /// Создает новую запись статистики.
        /// </summary>
        public async Task Create(Statistic model)
        {
            await _repositoryWrapper.Statistic.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующую запись статистики.
        /// </summary>
        public async Task Update(Statistic model)
        {
            _repositoryWrapper.Statistic.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет запись статистики по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var statistic = await _repositoryWrapper.Statistic
                .FindByCondition(x => x.StatisticId == id);

            _repositoryWrapper.Statistic.Delete(statistic.First());
            _repositoryWrapper.Save();
        }
    }
}