using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;

namespace BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Получает всех пользователей.
        /// </summary>
        public async Task<List<User>> GetAll()
        {
            return await _repositoryWrapper.User.FindAll();
        }

        /// <summary>
        /// Получает пользователя по ID.
        /// </summary>
        public async Task<User> GetById(int id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);

            return user.First();
        }

        /// <summary>
        /// Создает нового пользователя.
        /// </summary>
        public async Task Create(User model)
        {
            await _repositoryWrapper.User.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующего пользователя.
        /// </summary>
        public async Task Update(User model)
        {
            _repositoryWrapper.User.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет пользователя по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var user = await _repositoryWrapper.User
                .FindByCondition(x => x.UserId == id);

            _repositoryWrapper.User.Delete(user.First());
            _repositoryWrapper.Save();
        }
    }
}