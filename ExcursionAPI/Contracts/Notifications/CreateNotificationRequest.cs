namespace ExcursionAPI.Contracts.Notifications
{
    public class CreateNotificationRequest
    {
        public int UserID { get; set; }
        public string Message { get; set; }
    }
}
