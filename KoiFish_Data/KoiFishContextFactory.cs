using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFish_Data
{
    public class KoiFishContextFactory : IDesignTimeDbContextFactory<KoiFishDbContext>
    {
        public KoiFishDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                             .SetBasePath(Directory.GetCurrentDirectory())
                                             .AddJsonFile("appsettings.json")
                                             .Build();
            var builder = new DbContextOptionsBuilder<KoiFishDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new KoiFishDbContext(builder.Options);
        }    }
}
