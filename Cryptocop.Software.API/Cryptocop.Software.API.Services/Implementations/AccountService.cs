﻿using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Entities;
using Cryptocop.Software.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Cryptocop.Software.API.Repositories.Interfaces;


namespace Cryptocop.Software.API.Services.Implementations
{
    public class AccountService : IAccountService
    {
        IUserRepository _userRepository;
        ITokenService _tokenService;
        ITokenRepository _tokenRepository;

        public AccountService(IUserRepository userRepository, ITokenService tokenService, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _tokenService = tokenService;
        }
        public UserDto CreateUser(RegisterInputModel inputModel)
        {
            return _userRepository.CreateUser(inputModel);
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {
            var token = _tokenRepository.CreateNewToken();
            var user = _userRepository.AuthenticateUser(loginInputModel);
            user.TokenId = token.Id;
            return user;
        }

        public void Logout(int tokenId)
        {
            _tokenRepository.VoidToken(tokenId);
        }
    }
}