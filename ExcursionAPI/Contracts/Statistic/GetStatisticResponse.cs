namespace ExcursionAPI.Contracts.Statistic
{
    public class GetStatisticResponse
    {
        public int StatisticID { get; set; }
        public int TourID { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
