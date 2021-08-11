using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Products.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Products.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var productContext = scope.ServiceProvider.GetRequiredService<ProductContext>();

                    if (productContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        productContext.Database.Migrate();
                    }

                   ProductContextSeed.SeedAsync(productContext).Wait();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return host;
        }
    }
}
