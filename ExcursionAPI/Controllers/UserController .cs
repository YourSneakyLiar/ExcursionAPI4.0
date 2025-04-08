using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster; // Для маппинга объектов
using ExcursionAPI.Contracts.Users; // Для использования DTO (CreateUserRequest)
using System.Net.Mime;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями.
    /// </summary>
    /// <remarks>
    /// Предоставляет CRUD-операции для работы с пользователями:
    /// - Получение списка пользователей.
    /// - Получение пользователя по ID.
    /// - Создание нового пользователя.
    /// - Обновление данных пользователя.
    /// - Удаление пользователя.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера пользователей.
        /// </summary>
        /// <param name="userService">Сервис для работы с пользователями.</param>
        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        /// <summary>
        /// Получает список всех пользователей.
        /// </summary>
        /// <returns>Список всех пользователей в формате JSON.</returns>
        /// <response code="200">Успешно возвращен список пользователей.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetUserResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            var response = users.Adapt<List<GetUserResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает пользователя по его ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Пользователь в формате JSON или HTTP 404 Not Found, если пользователь не найден.</returns>
        /// <response code="200">Пользователь найден.</response>
        /// <response code="404">Пользователь с указанным ID не найден.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
                return NotFound();

            var response = user.Adapt<GetUserResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Добавляет нового пользователя.
        /// </summary>
        /// <param name="request">Объект CreateUserRequest, содержащий данные для создания пользователя.</param>
        /// <returns>HTTP 201 Created в случае успешного добавления.</returns>
        /// <response code="201">Пользователь успешно создан.</response>
        /// <response code="400">Некорректные данные запроса.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateUserRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var userDto = request.Adapt<User>();
            await _userService.Create(userDto);

            // Возвращаем статус 201 Created с указанием локации созданного ресурса
            return CreatedAtAction(nameof(GetById), new { id = userDto.UserId }, userDto.Adapt<GetUserResponse>());
        }

        /// <summary>
        /// Обновляет существующего пользователя.
        /// </summary>
        /// <param name="request">Объект UpdateUserRequest, содержащий обновленные данные.</param>
        /// <returns>HTTP 204 NoContent в случае успешного обновления.</returns>
        /// <response code="204">Пользователь успешно обновлен.</response>
        /// <response code="400">Некорректные данные запроса.</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var userDto = request.Adapt<User>();
            await _userService.Update(userDto);

            // Возвращаем статус 204 NoContent, так как обновление не требует ответа
            return NoContent();
        }

        /// <summary>
        /// Удаляет пользователя по его ID.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>HTTP 204 NoContent в случае успешного удаления.</returns>
        /// <response code="204">Пользователь успешно удален.</response>
        /// <response code="404">Пользователь с указанным ID не найден.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
                return NotFound();

            await _userService.Delete(id);

            // Возвращаем статус 204 NoContent, так как удаление не требует ответа
            return NoContent();
        }
    }
}