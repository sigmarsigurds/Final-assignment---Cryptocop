using System;
using System.Linq;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Contexts;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Cryptocop.Software.API.Repositories.Entities;
using Cryptocop.Software.API.Repositories.Helpers;


namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly CryptocopDbContext _dbContext;

        private readonly IMapper _mapper;
        public UserRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public UserDto CreateUser(RegisterInputModel inputModel)
        {

            var user_entity = _mapper.Map<User>(inputModel);
            user_entity.HashedPassword = HashingHelper.HashPassword(inputModel.Password);
            user_entity.ShoppingCart = new ShoppingCart { };
            _dbContext.Users.Add(user_entity);
            _dbContext.SaveChanges();

            var user_dto = _mapper.Map<UserDto>(user_entity);
            return user_dto;
        }

        public UserDto AuthenticateUser(LoginInputModel loginInputModel)
        {

            var passwordHash = HashingHelper.HashPassword(loginInputModel.Password);

            var user = _dbContext.Users.FirstOrDefault(u =>
                u.Email == loginInputModel.Email &&
                u.HashedPassword == passwordHash);
            if (user == null)
            {
                return null;
            }

            var token = new JwtToken();
            _dbContext.JwtTokens.Add(token);
            _dbContext.SaveChanges();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                TokenId = token.Id
            };
        }
    }
}