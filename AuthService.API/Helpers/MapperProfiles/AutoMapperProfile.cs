using AuthService.Entity;
using AutoMapper;
using RideSharing.Common.MessageQueues.Messages;

namespace AuthService.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, RegisterDto>();
            CreateMap<User, LoginDto>();
            CreateMap<User, UserRegistered>();
            CreateMap<User, UserModified>();
        }
    }
}
