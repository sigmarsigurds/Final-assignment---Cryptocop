using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryptocop.Software.API.Repositories.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System;
using Cryptocop.Software.API.Services.Helpers;
using System.Linq;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        ICryptoCurrencyService _cryptoService;
        IShoppingCartRepository _shoppingCartRepository;
        private HttpClient _httpClient;


        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, ICryptoCurrencyService cryptoService, HttpClient httpClient)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _httpClient = httpClient;
            _cryptoService = cryptoService;

        }
        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            return _shoppingCartRepository.GetCartItems(email);
        }

        public async Task AddCartItem(string email, ShoppingCartItemInputModel inputModel)
        {
            //todo: get unit price from other api

            // var httpMessage = await _httpClient.GetAsync($"assets/{inputModel.ProductIdentifier}");
            // var crypto = await HttpResponseMessageExtensions.DeserializeJsonToObject<CryptoCurrencyDto>(httpMessage);

            var cryptos = await _cryptoService.GetAvailableCryptocurrencies();
            var selectedCrypto = cryptos.FirstOrDefault(c => c.Symbol == inputModel.ProductIdentifier.ToUpper());

            _shoppingCartRepository.AddCartItem(email, inputModel, selectedCrypto.PriceInUsd);
        }

        public void RemoveCartItem(string email, int id)
        {
            _shoppingCartRepository.RemoveCartItem(email, id);
        }

        public void UpdateCartItemQuantity(string email, int id, ShoppingCartUpdateInputModel inputModel)
        {
            _shoppingCartRepository.UpdateCartItemQuantity(email, id, inputModel);
        }

        public void ClearCart(string email)
        {
            _shoppingCartRepository.ClearCart(email);
        }
    }
}
