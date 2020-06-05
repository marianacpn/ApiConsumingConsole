namespace Application.DTO
{
    public class AddressDTO
    {
        public string AddressCode { get; set; }

        public string AddressStreet { get; set; }

        public string District { get; set; }

        public string Locality { get; set; }

        public string FederalUnit { get; set; }

        public bool NewObject { get; set; }

        public override string ToString()
        {
            return $" CEP: {AddressCode},\n Logradouro: {AddressStreet},\n Bairro: {District},\n Localidade: {Locality},\n UF: {FederalUnit} \n";
        }
    }
}
