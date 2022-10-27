using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Repositories.Contexts;
using Cryptocop.Software.API.Repositories.Interfaces;
using Cryptocop.Software.API.Repositories.Entities;

namespace Cryptocop.Software.API.Repositories.Implementations
{
    public class AddressRepository : IAddressRepository
    {
        private readonly CryptocopDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddressRepository(CryptocopDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public AddressDto AddAddress(string email, AddressInputModel address)
        {
            var addressEntity = _mapper.Map<Address>(address);
            var user = _dbContext.Users.Include(u => u.Addresses).FirstOrDefault(u => u.Email == email);
            user.Addresses.Add(addressEntity);
            _dbContext.SaveChanges();
            return _mapper.Map<AddressDto>(addressEntity);
        }

        public IEnumerable<AddressDto> GetAllAddresses(string email)
        {
            var addresses = _dbContext.Users.Include(u => u.Addresses).FirstOrDefault(u => u.Email == email).Addresses;
            return _mapper.Map<ICollection<AddressDto>>(addresses);
        }

        public AddressDto DeleteAddress(string email, int addressId)
        {
            var address = _dbContext.Users.Include(u => u.Addresses).FirstOrDefault(u => u.Email == email).Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null) { return null; }
            _dbContext.Addresses.Remove(address);
            _dbContext.SaveChanges();
            return _mapper.Map<AddressDto>(address);
        }
    }
}