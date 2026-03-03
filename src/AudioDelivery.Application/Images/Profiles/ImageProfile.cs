using AudioDelivery.Application.Images.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Images.Profiles;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        this.CreateMap<Image, ImageDto>();
    }
}
