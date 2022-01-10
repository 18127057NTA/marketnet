using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.Store, o => o.MapFrom(s => s.Store.Name))
                .ForMember(d => d.StoreAddress, o => o.MapFrom(s => s.Store.Address))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.SupplierId, o => o.MapFrom(s => s.Store.Supplier.Id))
                .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Store.Supplier.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, BuyerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
        }
    }
}