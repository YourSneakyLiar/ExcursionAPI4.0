namespace ExcursionAPI.Contracts.TourLoadStatistics
{
    public class UpdateTourLoadStatisticRequest
    {
        public int StatisticID { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }
    }
}