namespace ExcursionAPI.Contracts.Orders
{
    public class CreateOrderRequest
    {
        public int UserID { get; set; }
        public int TourID { get; set; }
        public string Status { get; set; }
    }

}
