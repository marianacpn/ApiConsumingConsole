using Application.DTO;
using Application.Service.Interface;
using Domain.Entity;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Application.Service
{
    public class HttpClientService : IHttpClientService
    {
        public string URL => "https://viacep.com.br/ws/";

        public AddressDTO GetAddressByZipCodeApi(string option)
        {
            using (HttpClient httpClient = CreateNewHttpClient(URL))
            {
                using (HttpResponseMessage response = httpClient.GetAsync($"{option}/json/").GetAwaiter().GetResult())
                {
                    if (!response.IsSuccessStatusCode)
                        return null;

                    var responseHttp = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    var addressHttpContext =  JsonConvert.DeserializeObject<AddressDTOToHttpClient>(responseHttp);

                    if (string.IsNullOrEmpty(addressHttpContext.CEP))
                        return null;

                    return new AddressDTO
                    {
                        AddressCode = addressHttpContext.CEP,
                        AddressStreet = addressHttpContext.Logradouro,
                        District = addressHttpContext.Bairro,
                        FederalUnit = addressHttpContext.UF,
                        Locality = addressHttpContext.Localidade,
                        NewObject = true
                    };
                }
            }
        }

        private HttpClient CreateNewHttpClient(string URL)
        {
            HttpClient httpClient = new HttpClient
            {
                BaseAddress = new Uri(URL),
                Timeout = TimeSpan.FromMinutes(2)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();

            return httpClient;
        }
    }
}
