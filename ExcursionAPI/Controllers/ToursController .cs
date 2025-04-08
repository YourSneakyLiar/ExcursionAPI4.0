using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using ExcursionAPI.Contracts.Tours;
using System.Net.Mime;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления турами (экскурсиями)
    /// </summary>
    /// <remarks>
    /// Предоставляет CRUD-операции для работы с турами. 
    /// Позволяет получать информацию о доступных экскурсиях, создавать новые, обновлять и удалять существующие.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _toursService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера туров
        /// </summary>
        /// <param name="toursService">Сервис для работы с турами</param>
        public ToursController(ITourService toursService)
        {
            _toursService = toursService ?? throw new ArgumentNullException(nameof(toursService));
        }

        /// <summary>
        /// Получает список всех доступных туров
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/tours
        ///
        /// Пример ответа:
        /// [
        ///     {
        ///         "id": 1,
        ///         "title": "Обзорная экскурсия по городу",
        ///         "description": "Знакомство с основными достопримечательностями",
        ///         "durationHours": 3,
        ///         "price": 1500,
        ///         "category": "CityTour"
        ///     }
        /// ]
        /// </remarks>
        /// <returns>Список всех туров</returns>
        /// <response code="200">Успешно возвращен список туров</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetTourResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var tours = await _toursService.GetAll();
            var response = tours.Adapt<List<GetTourResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает детальную информацию о туре по его идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// GET /api/tours/5
        ///
        /// Пример ответа:
        /// {
        ///     "id": 5,
        ///     "title": "Экскурсия в музей",
        ///     "description": "Посещение главного городского музея",
        ///     "durationHours": 2,
        ///     "price": 800,
        ///     "category": "Museum"
        /// }
        /// </remarks>
        /// <param name="id">Идентификатор тура (целое число)</param>
        /// <returns>Данные о туре</returns>
        /// <response code="200">Тур найден</response>
        /// <response code="404">Тур с указанным ID не найден</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetTourResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var tour = await _toursService.GetById(id);

            if (tour == null)
                return NotFound();

            var response = tour.Adapt<GetTourResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создает новый тур
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// POST /api/tours
        /// {
        ///     "title": "Ночная экскурсия",
        ///     "description": "Осмотр города в ночных огнях",
        ///     "durationHours": 2.5,
        ///     "price": 2000,
        ///     "category": "NightTour"
        /// }
        ///
        /// Все поля обязательны для заполнения.
        /// </remarks>
        /// <param name="request">Данные для создания нового тура</param>
        /// <returns>Результат операции</returns>
        /// <response code="201">Тур успешно создан</response>
        /// <response code="400">Некорректные данные запроса</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateTourRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var tourDto = request.Adapt<Tour>();
            await _toursService.Create(tourDto);

            // Возвращаем статус 201 Created с указанием локации созданного ресурса
            return CreatedAtAction(nameof(GetById), new { id = tourDto.TourId }, tourDto.Adapt<GetTourResponse>());
        }

        /// <summary>
        /// Обновляет информацию о существующем туре
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// PUT /api/tours
        /// {
        ///     "id": 5,
        ///     "title": "Обновленная экскурсия в музей",
        ///     "description": "Расширенная программа посещения",
        ///     "durationHours": 3,
        ///     "price": 1000,
        ///     "category": "Museum"
        /// }
        ///
        /// Требуется указать полные данные тура.
        /// </remarks>
        /// <param name="request">Обновленные данные тура</param>
        /// <returns>Результат операции</returns>
        /// <response code="204">Тур успешно обновлен</response>
        /// <response code="400">Некорректные данные запроса</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateTourRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var tourDto = request.Adapt<Tour>();
            await _toursService.Update(tourDto);

            // Возвращаем статус 204 NoContent, так как обновление не требует ответа
            return NoContent();
        }

        /// <summary>
        /// Удаляет тур по его идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        /// DELETE /api/tours/5
        /// </remarks>
        /// <param name="id">Идентификатор удаляемого тура (целое число)</param>
        /// <returns>Результат операции</returns>
        /// <response code="204">Тур успешно удален</response>
        /// <response code="404">Тур с указанным ID не найден</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tour = await _toursService.GetById(id);

            if (tour == null)
                return NotFound();

            await _toursService.Delete(id);

            // Возвращаем статус 204 NoContent, так как удаление не требует ответа
            return NoContent();
        }
    }
}