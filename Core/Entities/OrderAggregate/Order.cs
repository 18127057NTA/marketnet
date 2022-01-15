namespace Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {
        }

        public Order(
            IReadOnlyList<OrderItem> orderItems,
            string buyerEmail,
            string buyerPhone,
            Address shipToAddress,
            DeliveryMethod deliveryMethod,
            int subtotal, // decimal ?
            string paymentIntentId
        )
        {
            BuyerEmail = buyerEmail;
            BuyerPhone = buyerPhone;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            Subtotal = subtotal;
            PaymentIntentId = paymentIntentId;
        }

        public string BuyerEmail { get; set; }
        public string BuyerPhone {get; set;}
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public int Subtotal { get; set; } // decimal
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }

        public int GetTotal() // decimal ?
        {
            return Subtotal + DeliveryMethod.Price;
        }
    }
}