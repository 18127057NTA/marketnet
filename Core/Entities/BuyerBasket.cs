namespace Core.Entities
{
    public class BuyerBasket
    {
        public BuyerBasket(){

        }

        public BuyerBasket(string id)
        { 
            Id = id; 
        }

        public string Id {get; set;} // Tại sao trong code lại là kiểu string?
        public List<BasketItem> Items {get; set;} = new List<BasketItem>();
        
        //public int? DeliveryMethodId { get; set; }
        //public string ClientSecret { get; set; }
        //public string PaymentIntentId { get; set; }
        //public decimal ShippingPrice { get; set; }
    }
}