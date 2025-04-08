namespace ExcursionAPI.Contracts.TourLoadStatistics
{
    public class CreateTourLoadStatisticRequest
    {
        public int TourID { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }
    }
}
