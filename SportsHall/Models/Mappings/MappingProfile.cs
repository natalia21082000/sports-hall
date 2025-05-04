using AutoMapper;
using SportsHall.Models;
using SportsHall.Models.Domains;
using SportsHall.Models.Entities;
using SportsHall.Common;

namespace SportsHall.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterDto, Users>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Roles.User));
            CreateMap<UserLoginDto, Users>();
            CreateMap<SportsHall.Models.Entities.Users, SportsHall.Models.Domains.UserReadDto>();
        }
    }
}
