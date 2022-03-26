using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class VaccineUrlResolver: IValueResolver<Vaccine, VaccineToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public VaccineUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Vaccine source, VaccineToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.HinhAnh))
            {
                return _config["ApiUrl"] + source.HinhAnh;
            }
            return null;
        }
    }
}