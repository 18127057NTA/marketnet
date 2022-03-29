using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Models
{
    public class TINH_THANH
    {
        public int TT_ID {get; set;}
        //public string TT_ID {get; set;}
        [Column(TypeName = "nvarchar")]
        public string TT_TENTT {get; set;}

    }
}