using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BuyerBasket
    {
        public BuyerBasket(){

        }

        public BuyerBasket(int id)
        { 
            Id = id; 
        }

        public int Id {get; set;} // Tại sao trong code lại là kiểu string?
        public List<BasketItem> Itmes {get; set;} = new List<BasketItem>();
        //
    }
}