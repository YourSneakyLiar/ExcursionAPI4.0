using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using ExcursionAPI.Contracts.Statistic;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Domain.Interfacess;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы со статистикой экскурсий
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Roles = "Admin,Analyst")] // Ограничение доступа
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера статистики
        /// </summary>
        /// <param name="statisticService">Сервис для работы со статистикой</param>
        public StatisticController(IStatisticService statisticService)
        {
            _statisticService = statisticService ?? throw new ArgumentNullException(nameof(statisticService));
        }

        /// <summary>
        /// Получает всю статистику экскурсий
        /// </summary>
        /// <returns>Список статистических данных</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetStatisticResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var statistics = await _statisticService.GetAll();
            var response = statistics.Adapt<List<GetStatisticResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает статистику по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор статистической записи</param>
        /// <returns>Статистические данные или 404 если не найдена</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetStatisticResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(int id)
        {
            var statistic = await _statisticService.GetById(id);
            if (statistic == null)
                return NotFound();

            var response = statistic.Adapt<GetStatisticResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Добавляет новую статистическую запись
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Statistic
        ///     {
        ///         "excursionId": 1,
        ///         "viewCount": 150,
        ///         "bookingCount": 30,
        ///         "averageRating": 4.8,
        ///         "statisticDate": "2023-06-01"
        ///     }
        /// </remarks>
        /// <param name="request">Данные для создания статистики</param>
        /// <returns>Статус 201 Created в случае успеха</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Add([FromBody] CreateStatisticRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var statistic = request.Adapt<Statistic>();
            await _statisticService.Create(statistic);

            // Возвращаем статус 201 Created с указанием локации созданного ресурса
            return CreatedAtAction(nameof(GetById), new { id = statistic.StatisticId }, statistic.Adapt<GetStatisticResponse>());
        }

        /// <summary>
        /// Обновляет существующую статистику
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Statistic
        ///     {
        ///         "id": 1,
        ///         "excursionId": 1,
        ///         "viewCount": 180,
        ///         "bookingCount": 35,
        ///         "averageRating": 4.9,
        ///         "statisticDate": "2023-06-01"
        ///     }
        /// </remarks>
        /// <param name="request">Обновленные статистические данные</param>
        /// <returns>Статус 204 NoContent в случае успеха</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateStatisticRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var statistic = request.Adapt<Statistic>();
            await _statisticService.Update(statistic);

            // Возвращаем статус 204 NoContent, так как обновление не требует ответа
            return NoContent();
        }

        /// <summary>
        /// Удаляет статистическую запись
        /// </summary>
        /// <param name="id">Идентификатор удаляемой статистики</param>
        /// <returns>Статус 204 NoContent в случае успеха</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            await _statisticService.Delete(id);

            // Возвращаем статус 204 NoContent, так как удаление не требует ответа
            return NoContent();
        }
    }
}