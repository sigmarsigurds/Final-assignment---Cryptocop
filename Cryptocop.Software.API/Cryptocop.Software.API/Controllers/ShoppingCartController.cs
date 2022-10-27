using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        [HttpGet]
        [Route("")]
        /* Gets all items within the shopping cart, see Models section for reference */
        public IActionResult getItems()
        {
            return Ok(_shoppingCartService.GetCartItems(User.Identity.Name));
        }

        [HttpPost]
        [Route("")]
        /* Adds an item to the shopping cart, see Models section for reference */
        public async Task<IActionResult> addItem([FromBody] ShoppingCartItemInputModel inputModel)
        {
            await _shoppingCartService.AddCartItem(User.Identity.Name, inputModel);
            return NoContent();
        }

        [HttpDelete]
        [Route("items/{id:int}")]
        /* Deletes an item from the shopping cart */
        public IActionResult deleteItem(int id)
        {
            _shoppingCartService.RemoveCartItem(User.Identity.Name, id);
            return NoContent();
        }

        [HttpPatch]
        [Route("items/{id:int}")]
        /* Updates the quantity for a shopping cart item */
        public IActionResult updateItemQuantity([FromBody] ShoppingCartUpdateInputModel inputModel, int id)
        {
            _shoppingCartService.UpdateCartItemQuantity(User.Identity.Name, id, inputModel);
            return NoContent();
        }

        [HttpDelete]
        [Route("")]
        /* Clears the cart - all items within the cart should be deleted */
        public IActionResult clearCart()
        {
            _shoppingCartService.ClearCart(User.Identity.Name);
            return NoContent();
        }
    }
}