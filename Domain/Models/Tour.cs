namespace Domain.Models;

public class Tour
{
    public int TourId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? ProviderId { get; set; }

    public int? GuideId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Complaint> Complaints { get; } = new List<Complaint>();

    public virtual User? Guide { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual User? Provider { get; set; }

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual ICollection<Statistic> Statistics { get; } = new List<Statistic>();

    public virtual ICollection<TourLoadStatistic> TourLoadStatistics { get; } = new List<TourLoadStatistic>();
}
