using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;

namespace BusinessLogic.Services
{
    public class TourService : ITourService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public TourService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Получает все туры.
        /// </summary>
        public async Task<List<Tour>> GetAll()
        {
            return await _repositoryWrapper.Tour.FindAll();
        }

        /// <summary>
        /// Получает тур по ID.
        /// </summary>
        public async Task<Tour> GetById(int id)
        {
            var tour = await _repositoryWrapper.Tour
                .FindByCondition(x => x.TourId == id);

            return tour.First();
        }

        /// <summary>
        /// Создает новый тур.
        /// </summary>
        public async Task Create(Tour model)
        {
            await _repositoryWrapper.Tour.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующий тур.
        /// </summary>
        public async Task Update(Tour model)
        {
            _repositoryWrapper.Tour.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет тур по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var tour = await _repositoryWrapper.Tour
                .FindByCondition(x => x.TourId == id);

            _repositoryWrapper.Tour.Delete(tour.First());
            _repositoryWrapper.Save();
        }
    }
}