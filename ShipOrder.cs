using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ShipOrder: BaseEntity // Mới thêm
    {
        //public int Id {get; set;}

        //[ForeignKey("OrderId")]
        public Order Order {get; set;} // lúc đầu t_order
        public int OrderId {get; set;}

        //[ForeignKey("ShipperId")]
        public Shipper Shipper {get; set;} // lúc đầu t_shipper
        public int ShipperId {get; set;}
    }
}