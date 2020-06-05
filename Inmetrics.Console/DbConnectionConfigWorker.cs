using Microsoft.Extensions.Options;
using Persistence.DbConfigurationJson;
using System;
using System.Threading.Tasks;

namespace Inmetrics.ConsoleApp
{
    public class DbConnectionConfigWorker
    {
        private readonly DbConnectionConfig _dbConnectionConfig;
        private string Server;
        private string Db;
        private string User;
        private string Password;
        public DbConnectionConfigWorker(IOptionsSnapshot<DbConnectionConfig> config)
        {
            _dbConnectionConfig = config.Value;
        }
        internal void ConfigureConnectionString()
        {
            ValidateConnectionString();

            Task.Delay(4000);

            Console.Clear();
        }

        private void ValidateConnectionString()
        {
            if (!_dbConnectionConfig.HasConnectionString())
            {
                Console.WriteLine($"\n======================\n" + 
                    "Configure a string de conexão: \n" +
                    $"\n======================\n" +
                    "Digite o Servidor: ");

                Server = Console.ReadLine();

                Console.WriteLine("Digite o Banco de Dados: ");

                Db = Console.ReadLine();

                Console.WriteLine("Digite o usuário: ");

                User = Console.ReadLine();

                Console.WriteLine("Digite a senha: ");

                Password = Console.ReadLine();

                _dbConnectionConfig.Configure(Server, Db, User, Password);

                Console.WriteLine($"\n======================\n" + 
                                   "String de conexão configurada!" +
                                   $"\n======================\n" );
            }
        }
    }
}
