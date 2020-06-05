using Application.App.Interface;
using Application.DTO;
using Application.Service.Interface;
using Application.Validator;
using AutoMapper;
using Domain.Entity;
using Domain.Interface;
using System;
using System.Collections.Generic;

namespace Application.App
{
    public class AppAddress : IAppAddress
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IAddressRepository _addressRepository;
        protected readonly IMapper _mapper;

        public AppAddress(IHttpClientService httpClientService,
            IAddressRepository addressRepository,
            IMapper mapper)
        {
            _httpClientService = httpClientService;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public IEnumerable<AddressDTO> GetAllAddresses() => _mapper.Map<List<AddressDTO>>(_addressRepository.GetAllAddresses());

        public AddressDTO GetAddressCode(string option)
        {
            option = option.Replace("-", "");

            if (!option.InputIsValid())
                Console.WriteLine($"\n======================\n" + 
                                  "Na opção de busca por CEP, digite o CEP apenas com números e/ou com separação por hífen." +
                                  $"\n======================\n");

            Address address = _addressRepository.GetAddressByZipCode(option);

            AddressDTO addressDTO = new AddressDTO();

            return address == null ? addressDTO = _httpClientService.GetAddressByZipCodeApi(option) : addressDTO = _mapper.Map<AddressDTO>(address);
        }

        public IEnumerable<AddressDTO> CreateNewAddress(AddressDTO addressDTO)
        {
            Address address = new Address(addressDTO.AddressCode, addressDTO.AddressStreet, addressDTO.District, addressDTO.Locality, addressDTO.FederalUnit);

            _addressRepository.Add(address);
            _addressRepository.SaveChanges();

            return _mapper.Map<IEnumerable<AddressDTO>>(_addressRepository.GetLast10Addresses());
        }
    }
}
