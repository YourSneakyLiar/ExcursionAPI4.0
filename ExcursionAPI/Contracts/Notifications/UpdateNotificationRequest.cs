namespace ExcursionAPI.Contracts.Notifications
{
    public class UpdateNotificationRequest
    {
        public int NotificationID { get; set; }
        public bool IsRead { get; set; }
    }
}