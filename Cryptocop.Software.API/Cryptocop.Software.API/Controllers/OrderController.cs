using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("")]
        /* Gets all orders associated with the authenticated user */
        public IActionResult getOrders()
        {
            return Ok(_orderService.GetOrders(User.Identity.Name));
        }

        [HttpPost]
        [Route("")]
        /* Adds a new order associated with the authenticated user, see Models section for reference */
        public IActionResult addOrder([FromBody] OrderInputModel inputModel)
        {
            return Ok(_orderService.CreateNewOrder(User.Identity.Name, inputModel));
        }
    }
}