namespace ExcursionAPI.Contracts.Notifications
{
    public class GetNotificationResponse
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
