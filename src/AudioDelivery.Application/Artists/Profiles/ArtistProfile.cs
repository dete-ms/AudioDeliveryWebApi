using AudioDelivery.Application.Artists.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Artists.Profiles;

public class ArtistProfile : Profile
{
    public ArtistProfile()
    {
        this.CreateMap<Artist, ArtistDto>()
            .ForMember(
                dto => dto.Genres, 
                opt => opt.MapFrom(a => a.Genres.Select(g => g.Name).ToList())
            );

        this.CreateMap<Artist, ArtistSummaryDto>();
    }
}
