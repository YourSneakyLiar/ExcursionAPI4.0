using Domain.Models;
using Domain.Interfaces;

namespace Domain.Wrapper
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IComplaintRepository Complaint { get; }
        INotificationRepository Notification { get; }
        IOrderRepository Order { get; }
        IProviderServiceRepository ProviderService { get; }
        IReviewRepository Review { get; }
        IRoleRepository Role { get; }
        IStatisticRepository Statistic { get; }
        ISubscriptionRepository Subscription { get; }
        ITourLoadStatisticRepository TourLoadStatistic { get; }
        ITourRepository Tour { get; }

        Task Save();
    }
}