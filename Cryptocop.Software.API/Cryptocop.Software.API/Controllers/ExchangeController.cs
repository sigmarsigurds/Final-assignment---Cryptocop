using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/exchanges")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        IExchangeService _exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet]
        [Route("{pageNumber:int}")]
        /* Gets all exchanges in a paginated envelope. This route
        accepts a single query parameter called pageNumber which is used to paginate the
        results */

        public async Task<IActionResult> getExchanges(int pageNumber)
        {
            var envelope = await _exchangeService.GetExchanges(pageNumber);

            return Ok(envelope.Items);
        }
    }
}