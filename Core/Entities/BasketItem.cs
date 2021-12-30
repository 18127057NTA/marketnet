namespace Core.Entities
{
    public class BasketItem
    {
        public int Id {get; set;}
        public string ProductName {get; set;}
        public int UnitPrice {get; set;}
        public int Quantity {get; set;}
        public string PictureUrl {get; set;}
        public string StoreName {get; set;}
        public string StoreAddress {get; set;}
        public string TypeName {get; set;}
        public string SupplierName {get; set;}

    }
}