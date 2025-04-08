namespace ExcursionAPI.Contracts.Subscriptions
{
    public class GetSubscriptionResponse
    {
        public int SubscriptionID { get; set; }
        public int UserID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
