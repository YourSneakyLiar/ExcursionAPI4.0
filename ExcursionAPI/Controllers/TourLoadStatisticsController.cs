using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using ExcursionAPI.Contracts.TourLoadStatistics;
using System.Net.Mime;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы со статистикой загрузки туров
    /// </summary>
    /// <remarks>
    /// Предоставляет методы для сбора и анализа данных о загрузке туров.
    /// Позволяет отслеживать популярность туров в разные периоды времени.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class TourLoadStatisticsController : ControllerBase
    {
        private readonly ITourLoadStatisticService _tourLoadStatisticService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера статистики загрузки туров
        /// </summary>
        /// <param name="tourLoadStatisticService">Сервис для работы со статистикой загрузки туров</param>
        public TourLoadStatisticsController(ITourLoadStatisticService tourLoadStatisticService)
        {
            _tourLoadStatisticService = tourLoadStatisticService ?? throw new ArgumentNullException(nameof(tourLoadStatisticService));
        }

        /// <summary>
        /// Получает всю статистику загрузки туров
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/tourloadstatistics
        ///
        /// Возвращает:
        /// [
        ///     {
        ///         "id": 1,
        ///         "tourId": 5,
        ///         "date": "2023-06-01",
        ///         "visitorsCount": 25,
        ///         "maxCapacity": 30,
        ///         "occupancyPercentage": 83.33
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Список статистических данных о загрузке туров</returns>
        /// <response code="200">Успешно возвращен список статистики</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetTourLoadStatisticResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tourLoadStatistics = await _tourLoadStatisticService.GetAll();
            var response = tourLoadStatistics.Adapt<List<GetTourLoadStatisticResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает статистику загрузки тура по идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/tourloadstatistics/5
        ///
        /// Возвращает:
        /// {
        ///     "id": 5,
        ///     "tourId": 10,
        ///     "date": "2023-06-02",
        ///     "visitorsCount": 18,
        ///     "maxCapacity": 20,
        ///     "occupancyPercentage": 90.0
        /// }
        /// </remarks>
        /// <param name="id">Идентификатор записи статистики</param>
        /// <returns>Данные о загрузке конкретного тура</returns>
        /// <response code="200">Статистика найдена</response>
        /// <response code="404">Запись статистики не найдена</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetTourLoadStatisticResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var tourLoadStatistic = await _tourLoadStatisticService.GetById(id);

            if (tourLoadStatistic == null)
                return NotFound();

            var response = tourLoadStatistic.Adapt<GetTourLoadStatisticResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Добавляет новую запись статистики загрузки тура
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /api/tourloadstatistics
        /// {
        ///     "tourId": 15,
        ///     "date": "2023-06-03",
        ///     "visitorsCount": 22,
        ///     "maxCapacity": 25
        /// }
        ///
        /// Поле occupancyPercentage будет рассчитано автоматически.
        /// </remarks>
        /// <param name="request">Данные для создания записи статистики</param>
        /// <returns>Результат операции</returns>
        /// <response code="201">Статистика успешно добавлена</response>
        /// <response code="400">Некорректные данные запроса</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateTourLoadStatisticRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var tourLoadStatisticDto = request.Adapt<TourLoadStatistic>();
            await _tourLoadStatisticService.Create(tourLoadStatisticDto);

            // Возвращаем статус 201 Created с указанием локации созданного ресурса
            return CreatedAtAction(nameof(GetById), new { id = tourLoadStatisticDto.TourId }, tourLoadStatisticDto.Adapt<GetTourLoadStatisticResponse>());
        }

        /// <summary>
        /// Обновляет существующую запись статистики загрузки тура
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /api/tourloadstatistics
        /// {
        ///     "id": 5,
        ///     "tourId": 10,
        ///     "date": "2023-06-02",
        ///     "visitorsCount": 19,
        ///     "maxCapacity": 20,
        ///     "occupancyPercentage": 95.0
        /// }
        /// </remarks>
        /// <param name="request">Обновленные данные статистики</param>
        /// <returns>Результат операции</returns>
        /// <response code="204">Статистика успешно обновлена</response>
        /// <response code="400">Некорректные данные запроса</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTourLoadStatisticRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var tourLoadStatisticDto = request.Adapt<TourLoadStatistic>();
            await _tourLoadStatisticService.Update(tourLoadStatisticDto);

            // Возвращаем статус 204 NoContent, так как обновление не требует ответа
            return NoContent();
        }

        /// <summary>
        /// Удаляет запись статистики загрузки тура
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /api/tourloadstatistics/5
        /// </remarks>
        /// <param name="id">Идентификатор удаляемой записи статистики</param>
        /// <returns>Результат операции</returns>
        /// <response code="204">Статистика успешно удалена</response>
        /// <response code="404">Запись статистики не найдена</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tourLoadStatistic = await _tourLoadStatisticService.GetById(id);

            if (tourLoadStatistic == null)
                return NotFound();

            await _tourLoadStatisticService.Delete(id);

            // Возвращаем статус 204 NoContent, так как удаление не требует ответа
            return NoContent();
        }
    }
}