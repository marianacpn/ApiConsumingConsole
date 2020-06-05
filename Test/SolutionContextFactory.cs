using CrossCutting.DI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.DbConfigurationJson;
using Persistence.EF;
using System;

namespace PortalCetelem.Test
{
    public class SolutionContextCoreFactory : IDesignTimeDbContextFactory<InmetricsContext>
    {
        public InmetricsContext CreateDbContext(string[] args)
        {
            IServiceCollection servicesCollection = new ServiceCollection();

            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development";
            var configuration = DIModule.ConfigureJson();

            servicesCollection.ConfigureClassesDI(configuration);

            var services = servicesCollection.BuildServiceProvider();

            using IServiceScope serviceScope = services.CreateScope();

            IOptionsSnapshot<DbConnectionConfig> options = serviceScope.ServiceProvider.GetService<IOptionsSnapshot<DbConnectionConfig>>();

            var builder = new DbContextOptionsBuilder<InmetricsContext>();
            builder.UseSqlServer(options.Value.GetConnectionString());

            return new InmetricsContext(options);
        }
    }
}
