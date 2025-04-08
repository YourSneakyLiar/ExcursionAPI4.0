namespace Domain.Models;


public class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? RoleId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Complaint> Complaints { get; } = new List<Complaint>();

    public virtual ICollection<Notification> Notifications { get; } = new List<Notification>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<ProviderService> ProviderServices { get; } = new List<ProviderService>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();

    public virtual ICollection<Tour> TourGuides { get; } = new List<Tour>();

    public virtual ICollection<Tour> TourProviders { get; } = new List<Tour>();
}
