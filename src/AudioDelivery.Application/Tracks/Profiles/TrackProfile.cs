using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Tracks.Profiles;

public class TrackProfile : Profile
{
    public TrackProfile()
    {
        this.CreateMap<Track, TrackDto>();

        this.CreateMap<CreateTrackRequest, Track>()
            .ForMember(dest => dest.Album, opt => opt.Ignore())
            .ForMember(dest => dest.Artists, opt => opt.Ignore());

        this.CreateMap<UpdateTrackRequest, Track>();
    }
}
