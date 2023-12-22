using AutoMapper;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Domain;

namespace AuthService.API.Helpers
{
    public class AutoMapperProfile : Profile
    {

        private IMapper _mapper;

        public IMapper GetMapperInstance()
        {
            if (_mapper == null)
            {
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(this);
                });

                _mapper = configuration.CreateMapper();
            }

            return _mapper;
        }

        public AutoMapperProfile()
        {
            CreateMap<UserRegistered, Customer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.AuthUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<UserRegistered, Driver>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.AuthUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<UserModified, Customer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.AuthUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<UserModified, Driver>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0))
                .ForMember(dest => dest.AuthUserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }

    }
}