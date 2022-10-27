﻿using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Repositories.Interfaces
{
    public interface IShoppingCartRepository
    {
        IEnumerable<ShoppingCartItemDto> GetCartItems(string email);
        void AddCartItem(string email, ShoppingCartItemInputModel shoppingCartItemItem, float UnitPrice);
        void RemoveCartItem(string email, int id);
        void UpdateCartItemQuantity(string email, int id, ShoppingCartUpdateInputModel shoppingCartItemInputModel);
        void ClearCart(string email);
        void DeleteCart(string email);
    }
}