using AutoMapper;
using Booking.Core.Dtos;
using Booking.Core.Entity;
using Job.Core.Entity;

namespace JOB_PORTALl_API.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductToreturnDto>()
                .ForMember(d => d.category, O => O.MapFrom(s => s.category.Name))
                .ForMember(d => d.User, O => O.MapFrom(s => s.User.UserName));

            CreateMap<Category,CategoryDto>().ReverseMap();

            CreateMap<AppUser, UserDto >();

            CreateMap<Order, OrderDto>()
                .ForMember(d => d.User, O => O.MapFrom(s => s.User.UserName))
                .ForMember(d => d.product, O => O.MapFrom(s => s.product.Name));

            CreateMap<Product, UpdateProductReq>().ReverseMap();

        }
    }
}
