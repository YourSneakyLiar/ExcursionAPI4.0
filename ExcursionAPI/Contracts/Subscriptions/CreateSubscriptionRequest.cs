namespace ExcursionAPI.Contracts.Subscriptions
{
    public class CreateSubscriptionRequest
    {
        public int UserID { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
    }
}
