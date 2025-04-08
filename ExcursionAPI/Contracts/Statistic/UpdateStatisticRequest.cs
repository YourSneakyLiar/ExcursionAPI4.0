namespace ExcursionAPI.Contracts.Statistic
{
    public class UpdateStatisticRequest
    {
        public int StatisticID { get; set; }
        public int TotalBookings { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}