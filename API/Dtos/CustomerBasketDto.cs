using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        //[Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }

        //public int? DeliveryMethodId { get; set; }
        //public string ClientSecret { get; set; }
        //public string PaymentIntentId { get; set; }
        //public decimal ShippingPrice { get; set; }
        public string VipMemberId {get; set;}
        public int PaymentTypeId {get; set;}
        public int Total {get; set;}
        public string TTChuyenKhoan {get; set;}
    }
}