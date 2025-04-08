namespace ExcursionAPI.Contracts.ProviderServices
{
    public class CreateProviderServiceRequest
    {
        public int ProviderID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
