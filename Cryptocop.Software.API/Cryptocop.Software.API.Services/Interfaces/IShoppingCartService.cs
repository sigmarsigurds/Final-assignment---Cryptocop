using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email);
        public Task AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemInputModel);
        public void RemoveCartItem(string email, int id);
        public void UpdateCartItemQuantity(string email, int id, ShoppingCartUpdateInputModel shoppingCartItemInputModel);
        public void ClearCart(string email);
    }
}