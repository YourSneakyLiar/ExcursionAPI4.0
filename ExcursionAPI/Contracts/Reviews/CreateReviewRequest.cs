namespace ExcursionAPI.Contracts.Reviews
{
    public class CreateReviewRequest
    {
        public int UserID { get; set; }
        public int TourID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
