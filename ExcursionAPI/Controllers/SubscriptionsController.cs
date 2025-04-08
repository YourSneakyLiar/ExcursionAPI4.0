using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using ExcursionAPI.Contracts.Subscriptions;
using System.Net.Mime;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления подписками на экскурсии
    /// </summary>
    /// <remarks>
    /// Позволяет создавать, просматривать, обновлять и удалять подписки пользователей на экскурсии.
    /// Для работы с API требуется аутентификация.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера подписок
        /// </summary>
        /// <param name="subscriptionService">Сервис для работы с подписками</param>
        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));
        }

        /// <summary>
        /// Получает список всех подписок
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/subscriptions
        /// 
        /// Возвращает:
        /// [
        ///     {
        ///         "id": 1,
        ///         "userId": 123,
        ///         "excursionId": 456,
        ///         "startDate": "2023-01-01T00:00:00",
        ///         "endDate": "2023-12-31T00:00:00"
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Список всех подписок в формате JSON</returns>
        /// <response code="200">Успешный запрос, возвращает список подписок</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetSubscriptionResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var subscriptions = await _subscriptionService.GetAll();
            var response = subscriptions.Adapt<List<GetSubscriptionResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает подписку по идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/subscriptions/5
        /// 
        /// Возвращает:
        /// {
        ///     "id": 5,
        ///     "userId": 123,
        ///     "excursionId": 456,
        ///     "startDate": "2023-01-01T00:00:00",
        ///     "endDate": "2023-12-31T00:00:00"
        /// }
        /// </remarks>
        /// <param name="id">Идентификатор подписки (целое число)</param>
        /// <returns>Данные подписки в формате JSON</returns>
        /// <response code="200">Подписка найдена</response>
        /// <response code="404">Подписка не найдена</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetSubscriptionResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var subscription = await _subscriptionService.GetById(id);

            if (subscription == null)
                return NotFound();

            var response = subscription.Adapt<GetSubscriptionResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создает новую подписку
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /api/subscriptions
        /// {
        ///     "userId": 123,
        ///     "excursionId": 456,
        ///     "startDate": "2023-01-01",
        ///     "endDate": "2023-12-31"
        /// }
        /// 
        /// Все поля обязательны для заполнения.
        /// </remarks>
        /// <param name="request">Данные для создания подписки</param>
        /// <returns>Результат операции</returns>
        /// <response code="201">Подписка успешно создана</response>
        /// <response code="400">Некорректные данные запроса</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateSubscriptionRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var subscription = request.Adapt<Subscription>();
            await _subscriptionService.Create(subscription);

            // Возвращаем статус 201 Created с указанием локации созданного ресурса
            return CreatedAtAction(nameof(GetById), new { id = subscription.SubscriptionId }, subscription.Adapt<GetSubscriptionResponse>());
        }

        /// <summary>
        /// Обновляет существующую подписку
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /api/subscriptions
        /// {
        ///     "id": 5,
        ///     "userId": 123,
        ///     "excursionId": 456,
        ///     "startDate": "2023-01-01",
        ///     "endDate": "2023-12-31"
        /// }
        /// 
        /// Требуется указать полные данные подписки.
        /// </remarks>
        /// <param name="request">Обновленные данные подписки</param>
        /// <returns>Результат операции</returns>
        /// <response code="204">Подписка успешно обновлена</response>
        /// <response code="400">Некорректные данные запроса</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateSubscriptionRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var subscription = request.Adapt<Subscription>();
            await _subscriptionService.Update(subscription);

            // Возвращаем статус 204 NoContent, так как обновление не требует ответа
            return NoContent();
        }

        /// <summary>
        /// Удаляет подписку по идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /api/subscriptions/5
        /// </remarks>
        /// <param name="id">Идентификатор подписки (целое число)</param>
        /// <returns>Результат операции</returns>
        /// <response code="204">Подписка успешно удалена</response>
        /// <response code="404">Подписка не найдена</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var subscription = await _subscriptionService.GetById(id);

            if (subscription == null)
                return NotFound();

            await _subscriptionService.Delete(id);

            // Возвращаем статус 204 NoContent, так как удаление не требует ответа
            return NoContent();
        }
    }
}