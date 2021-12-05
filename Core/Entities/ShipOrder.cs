using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ShipOrder
    {
        public int Id {get; set;}

        [ForeignKey("OrderId")]
        public Order t_order {get; set;}

        [ForeignKey("ShipperId")]
        public Shipper t_shipper {get; set;}
    }
}