using AutoMapper;
using BlogService.Entity;
using RideSharing.Common.MessageQueues.Messages;

namespace BlogService.API.Helpers.MapperProfiles
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
            CreateMap<UserRegistered, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));

            CreateMap<UserModified, User>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
