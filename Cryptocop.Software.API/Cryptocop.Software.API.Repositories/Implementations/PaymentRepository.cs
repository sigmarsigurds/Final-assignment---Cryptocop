using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Helpers;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Contexts;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Cryptocop.Software.API.Models.Exceptions;
using Cryptocop.Software.API.Repositories.Entities;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;
        public PaymentRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void AddPaymentCard(string email, PaymentCardInputModel paymentCard)
        {
            var user = _dbContext.Users.Include(u => u.CreditCards).FirstOrDefault(u => u.Email == email);

            if (user == null) { throw new ResourceNotFoundException("user not found"); }

            var cardEntity = _mapper.Map<PaymentCard>(paymentCard);
            user.CreditCards.Add(cardEntity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<PaymentCardDto> GetStoredPaymentCards(string email)
        {
            var user = _dbContext.Users.Include(u => u.CreditCards).FirstOrDefault(u => u.Email == email);

            if (user == null) { throw new ResourceNotFoundException("user not found"); }

            return _mapper.Map<ICollection<PaymentCardDto>>(user.CreditCards);
        }
    }
}