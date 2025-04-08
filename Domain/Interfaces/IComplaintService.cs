using Domain.Models;

namespace Domain.Interfaces
{
    public interface IComplaintService
    {
        Task<List<Complaint>> GetAll();
        Task<Complaint> GetById(int id);
        Task Create(Complaint model);
        Task Update(Complaint model);
        Task Delete(int id);
    }
}