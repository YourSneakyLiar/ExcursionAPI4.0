namespace ExcursionAPI.Contracts.Complaints
{
    public class UpdateComplaintRequest
    {
        public int ComplaintID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}