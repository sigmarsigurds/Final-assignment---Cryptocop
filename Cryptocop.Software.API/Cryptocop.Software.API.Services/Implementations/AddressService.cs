using System.Collections.Generic;
using Cryptocop.Software.API.Models.Dtos;
using Cryptocop.Software.API.Models.InputModels;
using Cryptocop.Software.API.Services.Interfaces;
using Cryptocop.Software.API.Repositories.Interfaces;

namespace Cryptocop.Software.API.Services.Implementations
{
    public class AddressService : IAddressService
    {
        IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public AddressDto AddAddress(string email, AddressInputModel address)
        {
            return _addressRepository.AddAddress(email, address);
        }

        public IEnumerable<AddressDto> GetAllAddresses(string email)
        {
            return _addressRepository.GetAllAddresses(email);
        }

        public AddressDto DeleteAddress(string email, int addressId)
        {
            return _addressRepository.DeleteAddress(email, addressId);
        }
    }
}