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
                dto => dto.TotalTracks,
                opt => opt.MapFrom(src => src.Tracks.Count)
            );

        this.CreateMap<Album, AlbumSummaryDto>()
            .ForMember(
                dto => dto.TotalTracks,
                opt => opt.MapFrom(src => src.Tracks.Count)
            );

        this.CreateMap<CreateAlbumRequest, Album>()
            .ForMember(dst => dst.Id, opt => opt.Ignore())
            .ForMember(dst => dst.Artists, opt => opt.Ignore())
            .ForMember(dst => dst.Images, opt => opt.Ignore())
            .ForMember(dst => dst.Tracks, opt => opt.Ignore());

        this.CreateMap<UpdateAlbumRequest, Album>();
    }
}
