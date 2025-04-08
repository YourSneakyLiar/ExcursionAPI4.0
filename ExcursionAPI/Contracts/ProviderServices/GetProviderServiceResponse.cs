namespace ExcursionAPI.Contracts.ProviderServices
{
    public class GetProviderServiceResponse
    {
        public int ServiceID { get; set; }
        public int ProviderID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
