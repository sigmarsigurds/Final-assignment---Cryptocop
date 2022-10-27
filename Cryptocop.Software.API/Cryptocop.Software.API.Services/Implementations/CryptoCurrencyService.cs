using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.Exceptions;
using Cryptocop.Software.API.Services.Helpers;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class CryptoCurrencyService : ICryptoCurrencyService
    {
        /*
        • Call the external API and get all cryptocurrencies with fields required for the
        CryptoCurrencyDto model

        • Deserializes the response to a list - I would advise to use the
        HttpResponseMessageExtensions which is located within Helpers/ to deserialize
        and flatten the response.

        • Return a filtered list where only the available cryptocurrencies BitCoin (BTC),
        Ethereum (ETH), Tether (USDT) and Monero (XMR) are within the list
        */
        private HttpClient _httpClient;
        public CryptoCurrencyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<CryptoCurrencyDto>> GetAvailableCryptocurrencies()
        {
            var allowedCurrencies = new List<string> { "Bitcoin", "Ethereum", "Tether", "Monero" };
            var allAssets = await _httpClient.GetAsync($"assets?fields=id,symbol,name,slug,metrics/market_data/price_usd,profile/general/overview/project_details");
            if (allAssets == null) { throw new ResourceNotFoundException(); }
            var crypto = await HttpResponseMessageExtensions.DeserializeJsonToList<CryptoCurrencyDto>(allAssets, true);

            var ret_list = crypto.Where(c => allowedCurrencies.Contains(c.Name));

            return ret_list;
        }
    }
}