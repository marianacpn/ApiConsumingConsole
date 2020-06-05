using Application.DTO;
using System.Collections.Generic;

namespace Application.App.Interface
{
    public interface IAppAddress
    {
        AddressDTO GetAddressCode(string option);
        IEnumerable<AddressDTO> GetAllAddresses();
        IEnumerable<AddressDTO> CreateNewAddress(AddressDTO addressDTO);
    }
}
