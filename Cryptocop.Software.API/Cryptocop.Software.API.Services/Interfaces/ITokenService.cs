using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Repositories.Entities;

namespace Cryptocop.Software.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(UserDto user);
    }
}