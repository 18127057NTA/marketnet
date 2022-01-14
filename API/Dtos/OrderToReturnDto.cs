using Core.Entities.OrderAggregate;

namespace API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public string PhoneNumber {get; set;} //?
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
		public int ShippingPrice { get; set; } // decimal?
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public int Subtotal { get; set; } // decimal?
        public int Total {get; set;} // decimal?
        public string Status { get; set; }
    }
}