using Domain.Entity;
using Domain.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Persistence.EF.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(InmetricsContext context) : base(context)
        {
        }

        public Address GetAddressByZipCode(string zipCode)
        {
            return ExecuteQuery(where: e => e.AddressCode.Replace("-", "").Trim().ToLower() == zipCode.Trim().ToLower());
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            return ExecuteQueryToList();
        }

        public IEnumerable<Address> GetLast10Addresses()
        {
            return ExecuteQueryToList(orderBy: e => e.OrderByDescending(e => e.Id), 
                                      take: 10);
        }
    }
}
