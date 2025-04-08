using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ExcursionAPI.Contracts.Orders;
using Mapster;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления заказами экскурсий
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера заказов
        /// </summary>
        /// <param name="orderService">Сервис для работы с заказами</param>
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        /// <summary>
        /// Получает список всех заказов
        /// </summary>
        /// <returns>Список заказов</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetOrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAll();
            var response = orders.Adapt<List<GetOrderResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает детали заказа по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Детали заказа или 404 если не найден</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetById(id);
            if (order == null)
                return NotFound();

            var response = order.Adapt<GetOrderResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создает новый заказ на экскурсию
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Orders
        ///     {
        ///         "excursionId": 1,
        ///         "userId": 1,
        ///         "participantsCount": 2,
        ///         "totalPrice": 5000,
        ///         "status": "Pending"
        ///     }
        /// </remarks>
        /// <param name="request">Данные для создания заказа</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateOrderRequest request)
        {
            if (request == null)
                return BadRequest("Request body is null");

            var order = request.Adapt<Order>();
            await _orderService.Create(order);
            return Ok();
        }

        /// <summary>
        /// Обновляет информацию о заказе
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Orders
        ///     {
        ///         "orderId": 1,
        ///         "excursionId": 1,
        ///         "userId": 1,
        ///         "participantsCount": 3,
        ///         "totalPrice": 7500,
        ///         "status": "Confirmed"
        ///     }
        /// </remarks>
        /// <param name="request">Обновленные данные заказа</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateOrderRequest request)
        {
            if (request == null)
                return BadRequest("Request body is null");

            var order = request.Adapt<Order>();
            await _orderService.Update(order);
            return Ok();
        }

        /// <summary>
        /// Удаляет заказ по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.Delete(id);
            return Ok();
        }
    }
}