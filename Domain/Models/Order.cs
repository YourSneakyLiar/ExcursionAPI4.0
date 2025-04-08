namespace Domain.Models;

public class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public int? TourId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Status { get; set; }

    public virtual Tour? Tour { get; set; }

    public virtual User? User { get; set; }
}
