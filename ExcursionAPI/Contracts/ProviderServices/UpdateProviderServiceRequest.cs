namespace ExcursionAPI.Contracts.ProviderServices
{
    public class UpdateProviderServiceRequest
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}