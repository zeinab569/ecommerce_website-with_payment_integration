namespace Core.Entities.OrderAggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
            
        }
        public Order(IReadOnlyList<OrderItem> items,string buyerEmail,
            Address shiptoaddress,
            DeliveryMethod _deliveryMethod,
            decimal subtotal
            )
        {
            Items = items;
            BuyerEmail = buyerEmail;
            ShipToAddress = shiptoaddress;
            deliveryMethod = _deliveryMethod;
            SubTotal = subtotal;
        }
        public string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod deliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> Items { get; set;}
        public decimal SubTotal { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
        {
            return SubTotal + deliveryMethod.Price;
        }
    }
}
