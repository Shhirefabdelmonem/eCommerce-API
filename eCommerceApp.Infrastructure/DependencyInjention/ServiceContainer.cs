using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Middleware;
using eCommerceApp.Infrastructure.Repos;
using eCommerceApp.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Builder;
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
                }).UseExceptionProcessor(),
                ServiceLifetime.Scoped);
            services.AddScoped<IGeneric<Product>, GenericRepo<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepo<Category>>();
            services.AddScoped(typeof(IAppLogger<>),typeof(SerilogLoggerAdapter<>));
            return services;
        }
        public static IApplicationBuilder UseInfrastructureService(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }

    }

}
