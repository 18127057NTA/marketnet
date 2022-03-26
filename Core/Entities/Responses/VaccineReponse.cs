using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Core.Entities.Responses
{
    public class VaccineReponse
    {
        public VaccineReponse(Vaccine vax)
        {
            if (vax != null)
            {
                Vax = vax;
            }
        }
        public VaccineReponse(int pageIndex, int pageSize, long count, IReadOnlyList<Vaccine> vaccines)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Vaccines = vaccines;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public long Count { get; set; }

        public IReadOnlyList<Vaccine> Vaccines { get; set; }

        public Vaccine Vax { get; set; }

    }

   
}