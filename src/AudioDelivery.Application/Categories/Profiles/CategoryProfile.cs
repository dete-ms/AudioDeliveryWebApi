using AudioDelivery.Application.Categories.DTOs;
using AudioDelivery.Domain.Entities;
using AutoMapper;

namespace AudioDelivery.Application.Categories.Profiles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        this.CreateMap<Category, CategoryDto>();
    }
}
