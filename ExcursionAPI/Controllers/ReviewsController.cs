using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ExcursionAPI.Contracts.Reviews;
using Mapster;
using System.Net.Mime;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления отзывами об экскурсиях
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера отзывов
        /// </summary>
        /// <param name="reviewService">Сервис для работы с отзывами</param>
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService ?? throw new ArgumentNullException(nameof(reviewService));
        }

        /// <summary>
        /// Получает список всех отзывов
        /// </summary>
        /// <returns>Список отзывов</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetReviewResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewService.GetAll();
            var response = reviews.Adapt<List<GetReviewResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает отзыв по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отзыва</param>
        /// <returns>Детали отзыва или 404 если не найден</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetReviewResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _reviewService.GetById(id);
            if (review == null)
                return NotFound();

            var response = review.Adapt<GetReviewResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создает новый отзыв
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Reviews
        ///     {
        ///         "excursionId": 1,
        ///         "userId": 1,
        ///         "rating": 5,
        ///         "comment": "Отличная экскурсия!",
        ///         "reviewDate": "2023-05-20T14:30:00"
        ///     }
        /// </remarks>
        /// <param name="request">Данные для создания отзыва</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateReviewRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var review = request.Adapt<Review>();
            await _reviewService.Create(review);
            return Ok();
        }

        /// <summary>
        /// Обновляет существующий отзыв
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Reviews
        ///     {
        ///         "reviewId": 1,
        ///         "excursionId": 1,
        ///         "userId": 1,
        ///         "rating": 4,
        ///         "comment": "Хорошая экскурсия, но дороговато",
        ///         "reviewDate": "2023-05-20T14:30:00"
        ///     }
        /// </remarks>
        /// <param name="request">Обновленные данные отзыва</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateReviewRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var review = request.Adapt<Review>();
            await _reviewService.Update(review);
            return Ok();
        }

        /// <summary>
        /// Удаляет отзыв по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемого отзыва</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _reviewService.Delete(id);
            return Ok();
        }
    }
}