using AudioDelivery.Application.Tracks.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Tracks.Profiles;

public class TrackProfile : Profile
{
    public TrackProfile()
    {
        this.CreateMap<Track, TrackDto>();
    }
}
