using AudioDelivery.Application.Albums.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Albums.Profiles;

public class AlbumProfile : Profile
{
    public AlbumProfile()
    {
        this.CreateMap<Album, AlbumDto>()
            .ForMember(
                dto => dto.AlbumType,
                opt => opt.MapFrom(src => src.AlbumType.ToString().ToLowerInvariant()))
            .ForMember(
                dto => dto.ReleaseDatePrecision,
                opt => opt.MapFrom(src => src.ReleaseDatePrecision.ToString().ToLowerInvariant()))
            .ForMember(
                dto => dto.TotalTracks,
                opt => opt.MapFrom(src => src.Tracks.Count)
            );

        this.CreateMap<Album, AlbumSummaryDto>()
            .ForMember(
                dto => dto.AlbumType,
                opt => opt.MapFrom(src => src.AlbumType.ToString().ToLowerInvariant()))
            .ForMember(
                dto => dto.TotalTracks,
                opt => opt.MapFrom(src => src.Tracks.Count)
            );

    }
}
