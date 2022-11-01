using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Cryptocop.Software.API.Repositories.Contexts;
using AutoMapper;
using Cryptocop.Software.API.Repositories.Entities;
using System;
using Cryptocop.Software.API.Models.Exceptions;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;
        public ShoppingCartRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<ShoppingCartItemDto> GetCartItems(string email)
        {
            var user = _dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(s => s.Items)
            .FirstOrDefault(u => u.Email == email);

            if (user == null) { throw new InvalidOperationException("no user found"); }

            return _mapper.Map<ICollection<ShoppingCartItemDto>>(user.ShoppingCart.Items);
        }

        public void AddCartItem(string email, ShoppingCartItemInputModel inputModel, float UnitPrice)
        {

            var user = _dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(s => s.Items)
            .FirstOrDefault(u => u.Email == email);


            if (user == null) { throw new InvalidOperationException($"no user found with email {email}"); }

            if (user.ShoppingCart == null) { throw new InvalidOperationException("no shoppingCart exists for this user"); }

            //var item = _mapper.Map<ShoppingCartItem>(inputModel);
            var item = new ShoppingCartItem
            {
                ProductIdentifier = inputModel.ProductIdentifier,
                Quantity = inputModel.Quantity ?? 0,
                UnitPrice = UnitPrice,

            };


            user.ShoppingCart.Items.Add(item);
            _dbContext.SaveChanges();
        }

        public void RemoveCartItem(string email, int id)
        {
            var user = _dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(s => s.Items)
            .FirstOrDefault(u => u.Email == email);

            var item = user.ShoppingCart.Items.FirstOrDefault(i => i.Id == id);
            user.ShoppingCart.Items.Remove(item);
            _dbContext.SaveChanges();
        }

        public void UpdateCartItemQuantity(string email, int id, ShoppingCartUpdateInputModel inputModel)
        {
            var user = _dbContext.Users
            .Include(u => u.ShoppingCart)
            .ThenInclude(s => s.Items)
            .FirstOrDefault(u => u.Email == email);

            var item = user.ShoppingCart.Items.FirstOrDefault(i => i.Id == id);
            if (item == null) { throw new ResourceNotFoundException(); }

            item.Quantity = inputModel.Quantity ?? 0;

            _dbContext.SaveChanges();
        }

        public void ClearCart(string email)
        {
            var user = _dbContext.Users
             .Include(u => u.ShoppingCart)
             .ThenInclude(s => s.Items)
             .FirstOrDefault(u => u.Email == email);

            foreach (var item in user.ShoppingCart.Items)
            {
                _dbContext.Remove(item);
            }
            _dbContext.SaveChanges();
        }

        public void DeleteCart(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}