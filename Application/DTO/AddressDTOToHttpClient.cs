using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class AddressDTOToHttpClient
    {
        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Bairro { get; set; }

        public string Localidade { get; set; }

        public string UF { get; set; }
    }
}
