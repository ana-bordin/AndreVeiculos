using Microsoft.EntityFrameworkCore;
using CarAPI.Client.Data;
using CarAPI.Address.Controllers;
using CarAPI.Address.Data;
using CarAPI.Address.Services;
using CarAPI.Address.Utils;
using Microsoft.Extensions.Options;
using CarAPI.Client.Services;
namespace CarAPI.Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddDbContext<CarAPIClientContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIClientContext") ?? throw new InvalidOperationException("Connection string 'CarAPIClientContext' not found.")));
            builder.Services.AddDbContext<CarAPIClientContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIClientContext") ?? throw new InvalidOperationException("Connection string 'CarAPIClientContext' not found.")));

            builder.Services.AddDbContext<CarAPIAddressContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIAddressContext") ?? throw new InvalidOperationException("Connection string 'CarAPIAddressContext' not found.")));

            // Registrar o AddressesController no contêiner de serviços
            builder.Services.AddTransient<AddressesController>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddControllers();
            builder.Services.AddControllers();

            builder.Services.Configure<ProjMongoDBAPIDataBaseSettings>(
                           builder.Configuration.GetSection(nameof(ProjMongoDBAPIDataBaseSettings)));

            builder.Services.AddSingleton<IProjMongoDBAPIDataBaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProjMongoDBAPIDataBaseSettings>>().Value);

            builder.Services.AddSingleton<ClientService>();
            //builder.Services.AddSingleton<AddressService>();

            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
