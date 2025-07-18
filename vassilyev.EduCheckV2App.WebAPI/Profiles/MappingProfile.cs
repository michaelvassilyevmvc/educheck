using AutoMapper;
using vassilyev.EduCheckV2App.WebAPI.Dto;
using vassilyev.EduCheckV2App.WebAPI.Entities;

namespace vassilyev.EduCheckV2App.WebAPI.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // UserCreateDto -> User
        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
            .ForMember(dest => dest.Sessions,
                opt => opt.Ignore());

        // User -> UserDto
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.SessionIds,
                opt => opt.MapFrom(src => src.Sessions.Select(s => s.Id)
                    .ToList()));

        // UserUpdateDto -> User
        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.Login,
                opt => opt.Condition(src => !string.IsNullOrEmpty(src.NewLogin)))
            .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.NewPassword)))
            .ForMember(dest => dest.Sessions,
                opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}