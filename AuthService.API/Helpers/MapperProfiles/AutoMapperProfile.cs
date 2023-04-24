using AutoMapper;
using RideSharing.Entity;

namespace AuthService.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDto>();
            CreateMap<User, LoginDto>();
        }
    }
}
