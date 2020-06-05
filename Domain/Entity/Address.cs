using System.ComponentModel;

namespace Domain.Entity
{
    public class Address
    {
        protected Address()
        {

        }
        public Address(string addressCode, string addressStreet, string district, string locality, string federalUnit)
        {
            AddressCode = addressCode;
            AddressStreet = addressStreet;
            District = district;
            Locality = locality;
            FederalUnit = federalUnit;
        }

        public int Id { get; private set; }

        public string AddressCode { get; private set; }

        public string AddressStreet { get; private set; }

        public string District { get; private set; }

        public string Locality { get; private set; }

        public string FederalUnit { get; private set; }
    }
}
