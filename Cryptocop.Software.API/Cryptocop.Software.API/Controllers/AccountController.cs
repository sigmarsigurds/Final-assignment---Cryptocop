using Microsoft.AspNetCore.Mvc;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;

namespace Cryptocop.Software.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService _accountService;
        ITokenService _tokenService;
        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        /* Registers a user within the application, see Models section for reference */
        public IActionResult addUser([FromBody] RegisterInputModel inputModel)
        {

            return Ok(_accountService.CreateUser(inputModel));
        }

        [HttpPost]
        [Route("signin")]
        /* Signs the user in by checking the credentials provided and issuing
         a JWT token in return, see Models section for reference */
        public IActionResult signIn([FromBody] LoginInputModel inputModel)
        {
            var user = _accountService.AuthenticateUser(inputModel);
            if (user == null) { return Unauthorized("user not found"); }
            return Ok(_tokenService.GenerateJwtToken(user));
        }

        [HttpGet]
        [Route("signout")]
        /* Logs the user out by voiding the provided JWT token
            using the id found within the claim */
        public IActionResult signOut()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "tokenId").Value, out var tokenId);
            _accountService.Logout(tokenId);
            return Ok(tokenId);
        }
    }
}