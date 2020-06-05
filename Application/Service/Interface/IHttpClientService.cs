using Application.DTO;

namespace Application.Service.Interface
{
    public interface IHttpClientService
    {
        AddressDTO GetAddressByZipCodeApi(string option);
    }
}
