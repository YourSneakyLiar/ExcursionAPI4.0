using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ReviewService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentNullException(nameof(repositoryWrapper));
        }

        /// <summary>
        /// Получает все отзывы.
        /// </summary>
        public async Task<List<Review>> GetAll()
        {
            return await _repositoryWrapper.Review.FindAll();
        }

        /// <summary>
        /// Получает отзыв по ID.
        /// </summary>
        public async Task<Review> GetById(int id)
        {
            var review = await _repositoryWrapper.Review
                .FindByCondition(x => x.ReviewId == id);

            if (!review.Any())
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            return review.First();
        }

        /// <summary>
        /// Создает новый отзыв.
        /// </summary>
        public async Task Create(Review model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Review model cannot be null.");
            }

            await _repositoryWrapper.Review.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующий отзыв.
        /// </summary>
        public async Task Update(Review model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model), "Review model cannot be null.");
            }

            var existingReview = await _repositoryWrapper.Review
                .FindByCondition(x => x.ReviewId == model.ReviewId);

            if (!existingReview.Any())
            {
                throw new KeyNotFoundException($"Review with ID {model.ReviewId} not found.");
            }

            _repositoryWrapper.Review.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет отзыв по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var review = await _repositoryWrapper.Review
                .FindByCondition(x => x.ReviewId == id);

            if (!review.Any())
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            _repositoryWrapper.Review.Delete(review.First());
            _repositoryWrapper.Save();
        }
    }
}