namespace ExcursionAPI.Contracts.Complaints
{
    public class CreateComplaintRequest
    {
        public int UserID { get; set; }
        public int TourID { get; set; }
        public string Description { get; set; }
    }
}
