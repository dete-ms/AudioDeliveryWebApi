using AudioDelivery.Application.Playlists.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Playlists.Profiles;

public class PlaylistTrackProfile : Profile
{
    public PlaylistTrackProfile()
    {
        CreateMap<PlaylistTrack, PlaylistTrackDto>()
            .ForMember(
                dto => dto.Track,
                opt => opt.MapFrom(src => src.Track))
            .ForMember(
                dto => dto.AddedBy,
                opt => opt.MapFrom(src => src.AddedByUser)
            );
    }
}
