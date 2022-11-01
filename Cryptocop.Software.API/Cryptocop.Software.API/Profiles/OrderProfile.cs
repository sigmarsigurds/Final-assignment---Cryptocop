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
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            //.ForMember(o => o.OrderItems, d => d.MapFrom(s => s.Items));
            CreateMap<OrderInputModel, Order>();
            CreateMap<ShoppingCartItemDto, OrderItemDto>();
            CreateMap<ShoppingCartItemDto, OrderItem>();
            CreateMap<OrderItem, ShoppingCartItemDto>();
        }

    }
}