namespace ExcursionAPI.Contracts.Tours
{
    public class CreateTourRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProviderID { get; set; }
        public int GuideID { get; set; }
    }
}
