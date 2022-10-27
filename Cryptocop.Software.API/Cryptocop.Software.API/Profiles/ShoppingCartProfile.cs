using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Repositories.Entities;

namespace Cryptocop.Software.API.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
                .ForMember(i => i.TotalPrice, o => o.MapFrom(s => s.UnitPrice * s.Quantity));
            CreateMap<ShoppingCartItemInputModel, ShoppingCartItem>();
        }
    }
}