namespace Domain.Models;

public class Subscription
{
    public int SubscriptionId { get; set; }

    public int? UserId { get; set; }

    public string Type { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User? User { get; set; }
}
