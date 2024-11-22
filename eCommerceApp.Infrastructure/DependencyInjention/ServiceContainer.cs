using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Infrastructure.DependencyInjention
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this  IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("cs"),
                sqlOptions =>
                {
                    //ensure this is the correct assembly
                    sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure();//enable automatic retries for transient farilure
                }),
                ServiceLifetime.Scoped);
            services.AddScoped<IGeneric<Product>, GenericRepo<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepo<Category>>();
            return services;
        }
    }
}
