using AudioDelivery.Application.Genres.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Genres.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        this.CreateMap<Genre, GenreDto>();
    }
}
