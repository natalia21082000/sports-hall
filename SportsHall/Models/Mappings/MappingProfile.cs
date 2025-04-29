using AutoMapper;
using SportsHall.Models;
using SportsHall.Models.Domains;
using SportsHall.Models.Entities;

namespace SportsHall.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, Users>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "User"));
            CreateMap<UserLoginDto, Users>();
        }
    }
}
