namespace ExcursionAPI.Contracts.Reviews
{
    public class UpdateReviewRequest
    {
        public int ReviewID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}