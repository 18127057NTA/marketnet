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
        //
    }
}