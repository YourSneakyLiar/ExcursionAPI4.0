using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ExcursionAPI.Contracts.Roles;
using Mapster;
using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления ролями пользователей
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Roles = "Admin")] // Ограничение доступа только для администраторов
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера ролей
        /// </summary>
        /// <param name="roleService">Сервис для работы с ролями</param>
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        /// <summary>
        /// Получает список всех ролей
        /// </summary>
        /// <returns>Список всех ролей системы</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetRoleResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAll();
            var response = roles.Adapt<List<GetRoleResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает роль по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор роли</param>
        /// <returns>Данные роли или 404 если не найдена</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GetRoleResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetById(id);
            if (role == null)
                return NotFound();

            var response = role.Adapt<GetRoleResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создает новую роль
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Roles
        ///     {
        ///         "name": "Moderator",
        ///         "description": "Модератор контента",
        ///         "permissions": ["content_edit", "user_ban"]
        ///     }
        /// </remarks>
        /// <param name="request">Данные для создания роли</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Add([FromBody] CreateRoleRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var role = request.Adapt<Role>();
            await _roleService.Create(role);
            return Ok();
        }

        /// <summary>
        /// Обновляет существующую роль
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Roles
        ///     {
        ///         "id": 1,
        ///         "name": "SeniorModerator",
        ///         "description": "Старший модератор контента",
        ///         "permissions": ["content_edit", "user_ban", "content_approve"]
        ///     }
        /// </remarks>
        /// <param name="request">Обновленные данные роли</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Update([FromBody] UpdateRoleRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var role = request.Adapt<Role>();
            await _roleService.Update(role);
            return Ok();
        }

        /// <summary>
        /// Удаляет роль по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемой роли</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.Delete(id);
            return Ok();
        }
    }
}