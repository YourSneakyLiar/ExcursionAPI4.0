using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public OrderService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentNullException(nameof(repositoryWrapper));
        }

        /// <summary>
        /// Получает все заказы.
        /// </summary>
        public async Task<List<Order>> GetAll()
        {
            return await _repositoryWrapper.Order.FindAll();
        }

        /// <summary>
        /// Получает заказ по ID.
        /// </summary>
        public async Task<Order> GetById(int id)
        {
            var order = await _repositoryWrapper.Order
                .FindByCondition(x => x.OrderId == id);

            if (!order.Any())
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            return order.First();
        }

        /// <summary>
        /// Создает новый заказ.
        /// </summary>
        public async Task Create(Order model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Order model cannot be null.");
            }

            await _repositoryWrapper.Order.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующий заказ.
        /// </summary>
        public async Task Update(Order model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Order model cannot be null.");
            }

            var existingOrder = await _repositoryWrapper.Order
                .FindByCondition(x => x.OrderId == model.OrderId);

            if (!existingOrder.Any())
            {
                throw new KeyNotFoundException($"Order with ID {model.OrderId} not found.");
            }

            _repositoryWrapper.Order.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет заказ по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var order = await _repositoryWrapper.Order
                .FindByCondition(x => x.OrderId == id);

            if (!order.Any())
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            _repositoryWrapper.Order.Delete(order.First());
            _repositoryWrapper.Save();
        }
    }
}