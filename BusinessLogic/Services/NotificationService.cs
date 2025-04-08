using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public NotificationService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentNullException(nameof(repositoryWrapper));
        }

        /// <summary>
        /// Получает все уведомления.
        /// </summary>
        public async Task<List<Notification>> GetAll()
        {
            return await _repositoryWrapper.Notification.FindAll();
        }

        /// <summary>
        /// Получает уведомление по ID.
        /// </summary>
        public async Task<Notification> GetById(int id)
        {
            var notification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id);

            if (!notification.Any())
            {
                throw new KeyNotFoundException($"Notification with ID {id} not found.");
            }

            return notification.First();
        }

        /// <summary>
        /// Создает новое уведомление.
        /// </summary>
        public async Task Create(Notification model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Notification model cannot be null.");
            }

            await _repositoryWrapper.Notification.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующее уведомление.
        /// </summary>
        public async Task Update(Notification model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Notification model cannot be null.");
            }

            var existingNotification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == model.NotificationId);

            if (!existingNotification.Any())
            {
                throw new KeyNotFoundException($"Notification with ID {model.NotificationId} not found.");
            }

            _repositoryWrapper.Notification.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет уведомление по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var notification = await _repositoryWrapper.Notification
                .FindByCondition(x => x.NotificationId == id);

            if (!notification.Any())
            {
                throw new KeyNotFoundException($"Notification with ID {id} not found.");
            }

            _repositoryWrapper.Notification.Delete(notification.First());
            _repositoryWrapper.Save();
        }
    }
}