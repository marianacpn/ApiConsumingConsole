using Application.App.Interface;
using Application.DTO;
using Application.Service.Interface;
using Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inmetrics.ConsoleApp
{
    public class ConsoleWorker
    {
        private IAppAddress _appAddress;
        private IHttpClientService _httpClientService;
        private readonly IAddressRepository _addressRepository;
        private readonly IServiceProvider _serviceProvider;

        private bool Running = true;

        public ConsoleWorker(IServiceProvider serviceProvider,
            IAddressRepository addressRepository)
        {
            _serviceProvider = serviceProvider;
            _addressRepository = addressRepository;
        }

        private void CreateScope()
        {
            var serviceScope = _serviceProvider.CreateScope();

            _appAddress = serviceScope.ServiceProvider.GetService<IAppAddress>();
            _httpClientService = serviceScope.ServiceProvider.GetService<IHttpClientService>();
        }

        internal void Run()
        {
            while (Running)
            {
                CreateScope();

                Console.WriteLine("====== Sistema de busca de CEPs (viacep.com.br) ======\n" +
                               "======================\n" +
                               "Digite a opção desejada: \n" +
                               "- Digite um CEP (Apenas números ou separação por hífen); \n" +
                               "- Digite 2 para ver todos os endereços salvos no banco; \n" +
                               "- Digite 3 para limpar console \n" +
                               "- Digite 0 para sair \n" +
                               "======================\n");

                string option = Console.ReadLine();

                Running = option switch
                {
                    "0" => false,
                    "2" => GetAllAddresses(),
                    "3" => ClearConsole(),
                    _ => GetAddressCode(option)
                };
            }
        }

        private bool ClearConsole()
        {
            Console.Clear();

            return true;
        }

        private bool GetAddressCode(string option)
        {
            if (option.Length < 8)
            {
                Console.WriteLine($"\n======================\n" + 
                                  "Na opção de busca por CEP, digite o CEP apenas com números e/ou com separação por hífen." +
                                  $"\n======================\n");
                return true;
            }

            var addressDTO = _appAddress.GetAddressCode(option);

            if (addressDTO == null)
                Console.WriteLine($"\n======================\n" + 
                                  "CEP não encontrado, verifique." +
                                  $"\n======================\n");

            else if (addressDTO.NewObject)
            {
                Console.WriteLine($"\n======================\n" + 
                                  $"Deseja salvar o novo endereço buscado no Banco? Sim (S) ou Não (N). \n" +
                                  $"\n======================\n" +
                                  $"{addressDTO.ToString()}" +
                                  $"\n======================\n");

                if (Console.ReadLine().Trim().ToLower() == "s")
                {
                    var addressDTOs = _appAddress.CreateNewAddress(addressDTO);

                    Console.WriteLine($"\n======================\n" +
                                      $"Endereço com CEP {addressDTO.AddressCode} salvo com sucesso. \n" +
                                      $"\n======================\n" +
                                      $"Abaixo estão os 10 últimos endereços salvos no banco: \n" +
                                      $"{string.Join("; \n\n", addressDTOs.Select(e => e.ToString()))}" +
                                      $"\n======================\n");
                }
            }

            else
                Console.WriteLine($"\n======================\n" + 
                                  $"{addressDTO.ToString()}" +
                                  $"\n======================\n");

            return true;
        }

        private bool GetAllAddresses()
        {
            IEnumerable<AddressDTO> addressDTOS = _appAddress.GetAllAddresses();

            if (addressDTOS.Count() == 0)
            {
                Console.WriteLine($"\n======================\n" + 
                                  "Não há registros de endereços no banco de dados." +
                                  $"\n======================\n");
                return true;
            }

            Console.WriteLine($"\n======================\n" + 
                             $"{string.Join(", \n\n", addressDTOS.Select(e => e.ToString()))}" + 
                             $"\n======================\n");

            return true;
        }
    }
}
