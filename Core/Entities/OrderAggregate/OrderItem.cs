namespace Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, int price, int quantity) // decimal price?
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered ItemOrdered { get; set; }
        public int Price { get; set; } // decimal
        public int Quantity { get; set; }
    }
}