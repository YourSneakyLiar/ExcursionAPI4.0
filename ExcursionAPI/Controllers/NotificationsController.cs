using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using ExcursionAPI.Contracts.Notifications;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с уведомлениями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        /// <summary>
        /// Конструктор контроллера уведомлений
        /// </summary>
        /// <param name="notificationService">Сервис для работы с уведомлениями</param>
        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        /// <summary>
        /// Получение списка всех уведомлений
        /// </summary>
        /// <returns>Список уведомлений</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notifications = await _notificationService.GetAll();
            var response = notifications.Adapt<List<GetNotificationResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получение уведомления по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор уведомления</param>
        /// <returns>Уведомление или 404 если не найдено</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var notification = await _notificationService.GetById(id);
            if (notification == null)
                return NotFound();

            var response = notification.Adapt<GetNotificationResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создание нового уведомления
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Notifications
        ///     {
        ///         "userId": 1,
        ///         "message": "У вас запланирована экскурсия",
        ///         "isRead": false
        ///     }
        /// </remarks>
        /// <param name="request">Данные для создания уведомления</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateNotificationRequest request)
        {
            var notification = request.Adapt<Notification>();
            await _notificationService.Create(notification);
            return Ok();
        }

        /// <summary>
        /// Обновление существующего уведомления
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Notifications
        ///     {
        ///         "notificationId": 1,
        ///         "userId": 1,
        ///         "message": "Экскурсия перенесена",
        ///         "isRead": true
        ///     }
        /// </remarks>
        /// <param name="request">Обновленные данные уведомления</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationRequest request)
        {
            var notification = request.Adapt<Notification>();
            await _notificationService.Update(notification);
            return Ok();
        }

        /// <summary>
        /// Удаление уведомления по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого уведомления</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationService.Delete(id);
            return Ok();
        }
    }
}