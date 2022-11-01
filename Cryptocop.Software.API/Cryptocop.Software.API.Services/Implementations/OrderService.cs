using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IShoppingCartService _shoppingCartService;

        public OrderService(IOrderRepository orderRepository, IShoppingCartService shoppingCartService)
        {
            _orderRepository = orderRepository;
            _shoppingCartService = shoppingCartService;

        }
        public IEnumerable<OrderDto> GetOrders(string email)
        {
            return _orderRepository.GetOrders(email);
        }

        public OrderDto CreateNewOrder(string email, OrderInputModel order)
        {
            var cartItems = _shoppingCartService.GetCartItems(email);
            _shoppingCartService.ClearCart(email);

            return _orderRepository.CreateNewOrder(email, order, cartItems);

        }
    }
}