using System;
using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Models.Exceptions;
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
            // var user = _dbContext.Users.Include(u => u.Orders).ThenInclude(o => o.Items).FirstOrDefault(u => u.Email == email);
            // var orderDto = _mapper.Map<ICollection<OrderDto>>(user.Orders);

            // return orderDto;
            ICollection<OrderDto> OrderDtos = new List<OrderDto>();
            var user = _dbContext.Users.Include(u => u.Orders).ThenInclude(o => o.Items).FirstOrDefault(u => u.Email == email);
            foreach (var order in user.Orders)
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                var orderItems = _mapper.Map<ICollection<OrderItemDto>>(order.Items);

                orderDto.OrderItems = orderItems;
                orderDto.CreditCard = order.MaskedCreditCard;
                OrderDtos.Add(orderDto);
            }
            return OrderDtos;
        }
        private ICollection<OrderItemDto> addOrderItem(string email, IEnumerable<ShoppingCartItemDto> cartItems, int orderId)
        {
            var user = _dbContext.Users
            .Include(u => u.Orders)
            .ThenInclude(o => o.Items)
            .FirstOrDefault(u => u.Email == email);


            if (user == null) { throw new ResourceNotFoundException("no user found"); }
            else if (user.Orders == null) { throw new ResourceNotFoundException("no Order exists for this user"); }

            var order = user.Orders.FirstOrDefault(o => o.Id == orderId);
            foreach (var item in cartItems)
            {
                var newOrderItem = new OrderItem
                {
                    ProductIdentifier = item.ProductIdentifier,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                };

                order.Items.Add(newOrderItem);

            }
            _dbContext.SaveChanges();
            var orderItems = _mapper.Map<ICollection<OrderItemDto>>(order.Items);
            return orderItems;

        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order, IEnumerable<ShoppingCartItemDto> cartItems)
        {
            var orderEntity = _mapper.Map<Order>(order);
            var user = _dbContext.Users
            .Include(u => u.Orders)
            .Include(u => u.Addresses)
            .Include(u => u.CreditCards)
            .FirstOrDefault(u => u.Email == email);

            var order_card = user.CreditCards.FirstOrDefault(c => c.Id == order.PaymentCardId);
            var order_address = user.Addresses.FirstOrDefault(a => a.Id == order.AddressId);

            orderEntity.Email = email;
            orderEntity.FullName = user.FullName;
            orderEntity.StreetName = order_address.StreetName;
            orderEntity.HouseNumber = order_address.HouseNumber;
            orderEntity.ZipCode = order_address.ZipCode;
            orderEntity.Country = order_address.Country;
            orderEntity.City = order_address.City;


            string replacement = "************";
            string maskedCard = replacement + order_card.CardNumber.Substring(8);
            orderEntity.CardHolderName = order_card.CardHolderName;
            orderEntity.MaskedCreditCard = maskedCard;

            orderEntity.OrderDate = DateTime.Now;

            foreach (var item in cartItems)
            {
                orderEntity.TotalPrice += item.UnitPrice;
            }

            user.Orders.Add(orderEntity);
            _dbContext.SaveChanges();

            var orderItemDtos = addOrderItem(email, cartItems, orderEntity.Id);
            var orderDto = _mapper.Map<OrderDto>(orderEntity);
            orderDto.CreditCard = order_card.CardNumber;
            orderDto.OrderItems = orderItemDtos;
            return orderDto;
        }
    }
}