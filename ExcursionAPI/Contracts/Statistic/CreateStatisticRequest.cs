namespace ExcursionAPI.Contracts.Statistic
{
    public class CreateStatisticRequest
    {
        public int TourID { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
