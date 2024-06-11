using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarAPI.Address.Data;
using CarAPI.Address.Services;
using CarAPI.Address.Utils;
using Microsoft.Extensions.Options;
namespace CarAPI.Address
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddDbContext<CarAPIAddressContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIAddressContext") ?? throw new InvalidOperationException("Connection string 'CarAPIAddressContext' not found.")));
            builder.Services.AddDbContext<CarAPIAddressContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIAddressContext") ?? throw new InvalidOperationException("Connection string 'CarAPIAddressContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            /////////////
            builder.Services.AddControllers();

            builder.Services.Configure<ProjMongoDBAPIDataBaseSettings>(
                           builder.Configuration.GetSection(nameof(ProjMongoDBAPIDataBaseSettings)));

            builder.Services.AddSingleton<IProjMongoDBAPIDataBaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProjMongoDBAPIDataBaseSettings>>().Value);

            builder.Services.AddSingleton<AddressService>();
            var app = builder.Build();
            //////////////
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
