using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;

namespace BusinessLogic.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public SubscriptionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Получает все подписки.
        /// </summary>
        public async Task<List<Subscription>> GetAll()
        {
            return await _repositoryWrapper.Subscription.FindAll();
        }

        /// <summary>
        /// Получает подписку по ID.
        /// </summary>
        public async Task<Subscription> GetById(int id)
        {
            var subscription = await _repositoryWrapper.Subscription
                .FindByCondition(x => x.SubscriptionId == id);

            return subscription.First();
        }

        /// <summary>
        /// Создает новую подписку.
        /// </summary>
        public async Task Create(Subscription model)
        {
            await _repositoryWrapper.Subscription.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующую подписку.
        /// </summary>
        public async Task Update(Subscription model)
        {
            _repositoryWrapper.Subscription.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет подписку по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var subscription = await _repositoryWrapper.Subscription
                .FindByCondition(x => x.SubscriptionId == id);

            _repositoryWrapper.Subscription.Delete(subscription.First());
            _repositoryWrapper.Save();
        }
    }
}