using AutoMapper;
using PizzaAsAService.BasketService.Api.Entities.Dtos;

namespace PizzaAsAService.BasketService.Api.Entities.MappingProfiles;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<UpdateBasketDto, Basket>();
    }
}