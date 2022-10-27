using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        [Route("")]
        /* Gets all addresses associated with authenticated user */
        public IActionResult getAddress()
        {
            System.Console.Write("name:");
            System.Console.Write(User.Identity.Name);
            return Ok(_addressService.GetAllAddresses(User.Identity.Name));
        }
        [HttpPost]
        [Route("")]
        /* Adds a new address associated with authenticated user, see
            Models section for reference */
        public IActionResult addAddress([FromBody] AddressInputModel inputModel)
        {
            return Ok(_addressService.AddAddress(User.Identity.Name, inputModel));
        }

        [HttpDelete]
        [Route("{id:int}")]
        /* Deletes an address by id */
        public IActionResult deleteAddress(int id)
        {
            if (_addressService.DeleteAddress(User.Identity.Name, id) == null)
            {
                return NotFound();
            }
            return Ok(_addressService.DeleteAddress(User.Identity.Name, id));
        }
    }
}