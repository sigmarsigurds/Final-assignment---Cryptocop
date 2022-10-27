using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers
{
    [ApiController]
    [Route("api/cryptocurrencies")]
    public class CryptoCurrencyController : ControllerBase
    {
        ICryptoCurrencyService _cryptoCurrencyService;
        public CryptoCurrencyController(ICryptoCurrencyService cryptoCurrencyService)
        {
            _cryptoCurrencyService = cryptoCurrencyService;
        }


        [HttpGet]
        [Route("")]
        /* Gets all available cryptocurrencies - the only available
            cryptocurrencies in this platform are BitCoin (BTC), Ethereum (ETH), Tether (USDT) and Monero (XMR) */
        public async Task<IActionResult> getCryptos()
        {
            var currencies = await _cryptoCurrencyService.GetAvailableCryptocurrencies();
            return Ok(currencies);
        }
    }
}
