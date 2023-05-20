using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Identity;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(a => a.ProductBrand, o => o.MapFrom(b => b.ProductBrand.Name))
                .ForMember(a => a.ProductType, o => o.MapFrom(b => b.ProductType.Name))
                .ForMember(a => a.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(a=>a.DeliveryMethod, o => o.MapFrom(b=>b.deliveryMethod.ShortName))
                .ForMember(a=>a.ShippingPrice, o=>o.MapFrom(b=>b.deliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(a => a.ProductId, o => o.MapFrom(b => b.ItemOrderd.ProductItemID))
                .ForMember(a => a.ProductName, o => o.MapFrom(b => b.ItemOrderd.ProductName))
                .ForMember(a => a.PictureUrl, o => o.MapFrom(b => b.ItemOrderd.PictureUrl))
                .ForMember(a => a.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>());

        }

    }
}
