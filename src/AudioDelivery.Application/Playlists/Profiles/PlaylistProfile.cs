using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Playlists.Profiles;

public class PlaylistProfile : Profile
{
    public PlaylistProfile()
    {
        this.CreateMap<Playlist, PlaylistDto>()
            .ForMember(
                dto => dto.Tracks,
                opt => opt.MapFrom(src => src.PlaylistTracks)
            );

        this.CreateMap<Playlist, PlaylistSummaryDto>();
    }
}
