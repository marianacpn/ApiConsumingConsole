using Domain.Entity;
using System.Collections.Generic;

namespace Domain.Interface
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        Address GetAddressByZipCode(string zipCode);
        IEnumerable<Address> GetAllAddresses();
        IEnumerable<Address> GetLast10Addresses();
    }
}
