using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProviderServiceService : IProviderServiceService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ProviderServiceService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentNullException(nameof(repositoryWrapper));
        }

        /// <summary>
        /// Получает все услуги провайдера.
        /// </summary>
        public async Task<List<ProviderService>> GetAll()
        {
            return await _repositoryWrapper.ProviderService.FindAll();
        }

        /// <summary>
        /// Получает услугу провайдера по ID.
        /// </summary>
        public async Task<ProviderService> GetById(int id)
        {
            var providerService = await _repositoryWrapper.ProviderService
                .FindByCondition(x => x.ServiceId == id);

            if (!providerService.Any())
            {
                throw new KeyNotFoundException($"Provider service with ID {id} not found.");
            }

            return providerService.First();
        }

        /// <summary>
        /// Создает новую услугу провайдера.
        /// </summary>
        public async Task Create(ProviderService model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Provider service model cannot be null.");
            }

            await _repositoryWrapper.ProviderService.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующую услугу провайдера.
        /// </summary>
        public async Task Update(ProviderService model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Provider service model cannot be null.");
            }

            var existingProviderService = await _repositoryWrapper.ProviderService
                .FindByCondition(x => x.ServiceId == model.ServiceId);

            if (!existingProviderService.Any())
            {
                throw new KeyNotFoundException($"Provider service with ID {model.ServiceId} not found.");
            }

            _repositoryWrapper.ProviderService.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет услугу провайдера по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var providerService = await _repositoryWrapper.ProviderService
                .FindByCondition(x => x.ServiceId == id);

            if (!providerService.Any())
            {
                throw new KeyNotFoundException($"Provider service with ID {id} not found.");
            }

            _repositoryWrapper.ProviderService.Delete(providerService.First());
            _repositoryWrapper.Save();
        }
    }
}