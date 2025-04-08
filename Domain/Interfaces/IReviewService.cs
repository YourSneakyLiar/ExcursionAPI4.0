using Domain.Models;

namespace Domain.Interfaces
{
    public interface IReviewService
    {
        Task<List<Review>> GetAll();
        Task<Review> GetById(int id);
        Task Create(Review model);
        Task Update(Review model);
        Task Delete(int id);
    }
}