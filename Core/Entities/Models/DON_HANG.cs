using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Models
{
    public class DON_HANG
    {
        public int DH_ID {get; set;}

        //id của chi nhánh tiêm
        public string DH_IDCN {get; set;}
        public int DH_IDKH {get; set;}
        public DateTime DH_NGAY {get; set;}

    }
}