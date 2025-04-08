using Domain.Interfaces;
using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using ExcursionAPI.Contracts.ProviderServices;
using System.Net.Mime;

namespace ExcursionAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления услугами провайдеров экскурсий
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ProviderServicesController : ControllerBase
    {
        private readonly IProviderServiceService _providerServiceService;

        /// <summary>
        /// Инициализирует новый экземпляр контроллера услуг провайдеров
        /// </summary>
        /// <param name="providerServiceService">Сервис для работы с услугами провайдеров</param>
        public ProviderServicesController(IProviderServiceService providerServiceService)
        {
            _providerServiceService = providerServiceService ?? throw new ArgumentNullException(nameof(providerServiceService));
        }

        /// <summary>
        /// Получает список всех услуг провайдеров
        /// </summary>
        /// <returns>Список услуг провайдеров</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetProviderServiceResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var providerServices = await _providerServiceService.GetAll();
            var response = providerServices.Adapt<List<GetProviderServiceResponse>>();
            return Ok(response);
        }

        /// <summary>
        /// Получает услугу провайдера по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор услуги провайдера</param>
        /// <returns>Детали услуги провайдера или 404 если не найдена</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetProviderServiceResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var providerService = await _providerServiceService.GetById(id);
            if (providerService == null)
                return NotFound();

            var response = providerService.Adapt<GetProviderServiceResponse>();
            return Ok(response);
        }

        /// <summary>
        /// Создает новую услугу провайдера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /ProviderServices
        ///     {
        ///         "name": "Экскурсия по музею",
        ///         "description": "Обзорная экскурсия по главному музею города",
        ///         "price": 1500,
        ///         "providerId": 1,
        ///         "category": "Museum"
        ///     }
        /// </remarks>
        /// <param name="request">Данные для создания услуги</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] CreateProviderServiceRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var providerService = request.Adapt<ProviderService>();
            await _providerServiceService.Create(providerService);
            return Ok();
        }

        /// <summary>
        /// Обновляет существующую услугу провайдера
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     PUT /ProviderServices
        ///     {
        ///         "id": 1,
        ///         "name": "Обновленная экскурсия",
        ///         "description": "Расширенная версия экскурсии",
        ///         "price": 2000,
        ///         "providerId": 1,
        ///         "category": "Premium"
        ///     }
        /// </remarks>
        /// <param name="request">Обновленные данные услуги</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateProviderServiceRequest request)
        {
            if (request == null)
                return BadRequest("Invalid request data.");

            var providerService = request.Adapt<ProviderService>();
            await _providerServiceService.Update(providerService);
            return Ok();
        }

        /// <summary>
        /// Удаляет услугу провайдера по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор удаляемой услуги</param>
        /// <returns>Статус 200 в случае успеха</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _providerServiceService.Delete(id);
            return Ok();
        }
    }
}