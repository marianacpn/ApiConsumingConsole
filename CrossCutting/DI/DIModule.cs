using Application.App;
using Application.App.Interface;
using Application.Service;
using Application.Service.Interface;
using AutoMapper;
using Domain.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbConfigurationJson;
using Persistence.EF;
using Persistence.EF.Repository;
using System;
using System.Linq;
using System.Reflection;

namespace CrossCutting.DI
{
    public static class DIModule
    {
        public static IConfigurationRoot ConfigureJson() => new ConfigurationBuilder()
                                                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                                .Build();

        public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ConfigureDbConnection(serviceCollection, configuration);
            ConfigureClassesDI(serviceCollection, configuration);

            return serviceCollection;
        }

        private static IServiceCollection ConfigureDbConnection(this IServiceCollection serviceCollection, IConfiguration configuration)
        {

            string connectionString = configuration.GetSection("DbConnection").GetValue<string>("IBSLaw");

            serviceCollection
                            .AddDbContext<InmetricsContext>();

            return serviceCollection;
        }

        public static IServiceCollection ConfigureClassesDI(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();

            serviceCollection.Configure<DbConnectionConfig>(options => configuration.GetSection("DbConnection").Bind(options));

            serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
            serviceCollection.AddScoped<IAppAddress, AppAddress>();
            serviceCollection.AddScoped<IHttpClientService, HttpClientService>();


            //Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "CrossCutting.Mapper");

            //Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
            //{
            //    return
            //      assembly.GetTypes()
            //              .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
            //              .ToArray();
            //}

            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());


            return serviceCollection;
        }
    }
}
