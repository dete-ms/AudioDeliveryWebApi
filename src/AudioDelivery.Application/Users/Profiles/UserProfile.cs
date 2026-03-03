using AudioDelivery.Application.Users.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Users.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        this.CreateMap<User, PublicUserDto>();
        this.CreateMap<User, UserProfileDto>();
    }
}
