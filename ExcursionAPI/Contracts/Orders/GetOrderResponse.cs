namespace ExcursionAPI.Contracts.Orders
{
    public class GetOrderResponse
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int TourID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
