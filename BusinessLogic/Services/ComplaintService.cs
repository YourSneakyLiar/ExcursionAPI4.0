using Domain.Interfaces;
using Domain.Models;
using Domain.Wrapper;

namespace BusinessLogic.Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public ComplaintService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        /// <summary>
        /// Получает все жалобы.
        /// </summary>
        public async Task<List<Complaint>> GetAll()
        {
            return await _repositoryWrapper.Complaint.FindAll();
        }

        /// <summary>
        /// Получает жалобу по ID.
        /// </summary>
        public async Task<Complaint> GetById(int id)
        {
            var complaint = await _repositoryWrapper.Complaint
                .FindByCondition(x => x.ComplaintId == id);

            return complaint.First();
        }

        /// <summary>
        /// Создает новую жалобу.
        /// </summary>
        public async Task Create(Complaint model)
        {
            await _repositoryWrapper.Complaint.Create(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Обновляет существующую жалобу.
        /// </summary>
        public async Task Update(Complaint model)
        {
            _repositoryWrapper.Complaint.Update(model);
            _repositoryWrapper.Save();
        }

        /// <summary>
        /// Удаляет жалобу по ID.
        /// </summary>
        public async Task Delete(int id)
        {
            var complaint = await _repositoryWrapper.Complaint
                .FindByCondition(x => x.ComplaintId == id);

            _repositoryWrapper.Complaint.Delete(complaint.First());
            _repositoryWrapper.Save();
        }
    }
}