namespace ExcursionAPI.Contracts.Reviews
{
    public class GetReviewResponse
    {
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        public int TourID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
