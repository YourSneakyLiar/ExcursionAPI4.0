namespace ExcursionAPI.Contracts.Orders
{
    public class UpdateOrderRequest
    {
        public int OrderID { get; set; }
        public string Status { get; set; }
    }
}