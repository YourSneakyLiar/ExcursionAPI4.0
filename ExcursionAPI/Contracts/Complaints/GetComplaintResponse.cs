namespace ExcursionAPI.Contracts.Complaints
{
    public class GetComplaintResponse
    {
        public int ComplaintID { get; set; }
        public int UserID { get; set; }
        public int TourID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
