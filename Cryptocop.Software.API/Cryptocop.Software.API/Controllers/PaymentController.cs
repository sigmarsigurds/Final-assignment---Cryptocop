using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        [Route("")]
        /* Gets all payment cards associated with the authenticated user */
        public IActionResult getCards()
        {
            return Ok(_paymentService.GetStoredPaymentCards(User.Identity.Name));
        }

        [HttpPost]
        [Route("")]
        /* Adds a new payment card associated with the authenticated user, see Models section for reference */
        public IActionResult addCard([FromBody] PaymentCardInputModel inputModel)
        {
            _paymentService.AddPaymentCard(User.Identity.Name, inputModel);
            return NoContent();
        }
    }
}