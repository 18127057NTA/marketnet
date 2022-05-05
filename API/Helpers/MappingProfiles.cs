using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

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
            //CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, BuyerBasket>()
                .ForMember(d => d.Total, o => o.MapFrom(s => s.Total))
                .ForMember(d => d.VipMemberId, o => o.MapFrom(s => s.VipMemberId))
                .ForMember(d => d.TTChuyenKhoan, o => o.MapFrom(s => s.TTChuyenKhoan));
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

            CreateMap<Vaccine, VaccineToReturnDto>()
                .ForMember(v => v.HinhAnh, o => o.MapFrom<VaccineUrlResolver>());
        }
    }
}