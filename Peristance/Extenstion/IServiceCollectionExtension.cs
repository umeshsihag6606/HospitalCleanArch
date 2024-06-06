using Application.Interfaces.GenericRepositories;
using Application.Interfaces.UnitofworkRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Peristance.DataContexts;
using Peristance.Extenstion.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peristance.Extenstion
{
    public static class IServiceCollectionExtension
    {
      public static void AddPeristanceLayer(this IServiceCollection services,IConfiguration configuration )
        {
            services.AddDbContext(configuration);
            services.AddRepositry();
        }
        public static void AddDbContext(this IServiceCollection services,IConfiguration configuration)
        {
            var Connectionstring = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationdbContext>(options=>
            options.UseSqlServer(Connectionstring,
            builder=>builder.MigrationsAssembly(typeof(ApplicationdbContext).Assembly.FullName)));
        }
        public static void AddRepositry(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork),typeof(UnitOfWork))
                .AddTransient(typeof(IGenericRepositary<>),typeof(GenricRepository<>))
                .AddHttpContextAccessor();
        }
    }
}
