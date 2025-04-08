using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ExcursionAPI.Contracts.Complaints;
using Mapster;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с жалобами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintService _complaintService;

        /// <summary>
        /// Конструктор контроллера жалоб
        /// </summary>
        /// <param name="complaintService">Сервис для работы с жалобами</param>
        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService ?? throw new ArgumentNullException(nameof(complaintService));
        }

        /// <summary>
        /// Получение списка всех жалоб
        /// </summary>
        /// <returns>Список жалоб</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetComplaintResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var complaints = await _complaintService.GetAll();
            var response = complaints.Adapt<List<GetComplaintResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получение жалобы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор жалобы</param>
        /// <returns>Жалоба или 404 если не найдена</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetComplaintResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var complaint = await _complaintService.GetById(id);
            if (complaint == null)
                return NotFound();

            var response = complaint.Adapt<GetComplaintResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создание новой жалобы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Complaints
        ///     {
        ///         "title": "Некорректное поведение гида",
        ///         "description": "Гид опаздывал на экскурсии",
        ///         "excursionId": 1,
        ///         "userId": 1
        ///     }
        /// </remarks>
        /// <param name="request">Данные для создания жалобы</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateComplaintRequest request)
        {
            if (request == null)
                return BadRequest("Request body is null");

            var complaint = request.Adapt<Complaint>();
            await _complaintService.Create(complaint);
            return Ok();
        }

        /// <summary>
        /// Обновление существующей жалобы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /Complaints
        ///     {
        ///         "complaintId": 1,
        ///         "title": "Обновленная жалоба",
        ///         "description": "Обновленное описание проблемы",
        ///         "status": "InProgress",
        ///         "excursionId": 1,
        ///         "userId": 1
        ///     }
        /// </remarks>
        /// <param name="request">Обновленные данные жалобы</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateComplaintRequest request)
        {
            if (request == null)
                return BadRequest("Request body is null");

            var complaint = request.Adapt<Complaint>();
            await _complaintService.Update(complaint);
            return Ok();
        }

        /// <summary>
        /// Удаление жалобы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемой жалобы</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _complaintService.Delete(id);
            return Ok();
        }
    }
}