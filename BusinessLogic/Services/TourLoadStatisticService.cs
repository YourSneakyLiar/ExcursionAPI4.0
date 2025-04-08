using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;

namespace BusinessLogic.Services
{
    public class TourLoadStatisticService : ITourLoadStatisticService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public TourLoadStatisticService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Получает всю статистику загрузки туров.
        /// </summary>
        public async Task<List<TourLoadStatistic>> GetAll()
        {
            return await _repositoryWrapper.TourLoadStatistic.FindAll();
        }

        /// <summary>
        /// Получает статистику загрузки туров по ID.
        /// </summary>
        public async Task<TourLoadStatistic> GetById(int id)
        {
            var tourLoadStatistic = await _repositoryWrapper.TourLoadStatistic
                .FindByCondition(x => x.StatisticId == id);

            return tourLoadStatistic.First();
        }

        /// <summary>
        /// Создает новую запись статистики загрузки туров.
        /// </summary>
        public async Task Create(TourLoadStatistic model)
        {
            await _repositoryWrapper.TourLoadStatistic.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующую запись статистики загрузки туров.
        /// </summary>
        public async Task Update(TourLoadStatistic model)
        {
            _repositoryWrapper.TourLoadStatistic.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет запись статистики загрузки туров по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var tourLoadStatistic = await _repositoryWrapper.TourLoadStatistic
                .FindByCondition(x => x.StatisticId == id);

            _repositoryWrapper.TourLoadStatistic.Delete(tourLoadStatistic.First());
            _repositoryWrapper.Save();
        }
    }
}