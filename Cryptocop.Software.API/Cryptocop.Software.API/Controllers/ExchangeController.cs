using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/exchanges")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        public ExchangeController()
        {

        }

        [HttpGet]
        [Route("")]
        /* Gets all exchanges in a paginated envelope. This routes
        accepts a single query parameter called pageNumber which is used to paginate the
        results */

        //TODO: ADD QUERY PARAMETRES
        public IActionResult getExchanges()
        {
            throw new NotImplementedException();
        }
    }
}