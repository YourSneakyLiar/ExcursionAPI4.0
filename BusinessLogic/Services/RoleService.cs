using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public RoleService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentNullException(nameof(repositoryWrapper));
        }

        /// <summary>
        /// Получает все роли.
        /// </summary>
        public async Task<List<Role>> GetAll()
        {
            return await _repositoryWrapper.Role.FindAll();
        }

        /// <summary>
        /// Получает роль по ID.
        /// </summary>
        public async Task<Role> GetById(int id)
        {
            var role = await _repositoryWrapper.Role
                .FindByCondition(x => x.RoleId == id);

            if (!role.Any())
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }

            return role.First();
        }

        /// <summary>
        /// Создает новую роль.
        /// </summary>
        public async Task Create(Role model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Role model cannot be null.");
            }

            await _repositoryWrapper.Role.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующую роль.
        /// </summary>
        public async Task Update(Role model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Role model cannot be null.");
            }

            var existingRole = await _repositoryWrapper.Role
                .FindByCondition(x => x.RoleId == model.RoleId);

            if (!existingRole.Any())
            {
                throw new KeyNotFoundException($"Role with ID {model.RoleId} not found.");
            }

            _repositoryWrapper.Role.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет роль по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var role = await _repositoryWrapper.Role
                .FindByCondition(x => x.RoleId == id);

            if (!role.Any())
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }

            _repositoryWrapper.Role.Delete(role.First());
            _repositoryWrapper.Save();
        }
    }
}