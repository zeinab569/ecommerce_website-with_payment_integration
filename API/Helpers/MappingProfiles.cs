using API.Dtos;
using AutoMapper;
using Core.Entities;
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

            CreateMap<Address,AddressDto>().ReverseMap();
            CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();

        }

    }
}
