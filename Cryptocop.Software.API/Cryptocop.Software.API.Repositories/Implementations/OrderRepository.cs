using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Repositories.Entities;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<OrderDto> GetOrders(string email)
        {
            var user = _dbContext.Users.Include(u => u.Orders).FirstOrDefault(u => u.Email == email);
            return _mapper.Map<ICollection<OrderDto>>(user.Orders);
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {
            var orderEntity = _mapper.Map<Order>(order);
            var user = _dbContext.Users.Include(u => u.Orders).FirstOrDefault(u => u.Email == email);
            user.Orders.Add(orderEntity);
            _dbContext.SaveChanges();
            return _mapper.Map<OrderDto>(orderEntity);
        }
    }
}