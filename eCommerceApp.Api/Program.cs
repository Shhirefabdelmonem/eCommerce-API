using eCommerceApp.Application.DependencyInjection;
using eCommerceApp.Infrastructure.DependencyInjention;
using Serilog;

namespace eCommerceApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger=new LoggerConfiguration().Enrich.FromLogContext().
                WriteTo.Console().
                WriteTo.File("log/log.txt",rollingInterval:RollingInterval.Day).
                CreateLogger();

            builder.Host.UseSerilog();
            Log.Logger.Information("Apllication is building ......");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddCors(builder =>
            {
                builder.AddDefaultPolicy(options =>
                {
                    options.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("https://localhost:7025")
                    .AllowCredentials();
                });
            });
            try
            {
                var app = builder.Build();
                app.UseCors();
                app.UseSerilogRequestLogging();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseInfrastructureService();
                app.UseHttpsRedirection();

                app.UseAuthorization();


                app.MapControllers();
                Log.Logger.Information("Application is running ......");
                app.Run();
            }
            catch (Exception ex) 
            {
                Log.Logger.Error(ex, "Application is failed to start ......");
            }

            finally
            {
                Log.CloseAndFlush();
            }

            
        }
    }
}
