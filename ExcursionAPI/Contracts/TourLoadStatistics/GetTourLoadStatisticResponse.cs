namespace ExcursionAPI.Contracts.TourLoadStatistics
{
    public class GetTourLoadStatisticResponse
    {
        public int StatisticID { get; set; }
        public int TourID { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
