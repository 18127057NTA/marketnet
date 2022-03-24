using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Responses
{
    public class VaccineReponse
    {
        public VaccineReponse(int pageIndex, int pageSize, int count, IReadOnlyList<Vaccine> vaccines){
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Vaccines = vaccines;
        }

        public int PageIndex {get; set;}
        public int PageSize {get; set;}

        public int Count {get; set;}

        public IReadOnlyList<Vaccine> Vaccines {get; set;}

    }
}