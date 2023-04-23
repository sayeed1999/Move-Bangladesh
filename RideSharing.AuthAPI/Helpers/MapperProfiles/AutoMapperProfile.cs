using AutoMapper;
using RideSharing.Entity;

namespace RideSharing.AuthAPI.Helpers
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
