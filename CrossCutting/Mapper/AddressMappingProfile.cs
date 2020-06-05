using Application.DTO;
using AutoMapper;
using Domain.Entity;

namespace CrossCutting.Mapper
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressDTO>();
        }
    }
}
