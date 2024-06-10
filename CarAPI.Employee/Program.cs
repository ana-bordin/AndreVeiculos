using Microsoft.EntityFrameworkCore;
using CarAPI.Address.Data;
using CarAPI.Employee.Data;
namespace CarAPI.Employee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<CarAPIEmployeeContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIEmployeeContext") ?? throw new InvalidOperationException("Connection string 'CarAPIEmployeeContext' not found.")));

            builder.Services.AddDbContext<CarAPIAddressContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIAddressContext") ?? throw new InvalidOperationException("Connection string 'CarAPIAddressContext' not found.")));

            //builder.Services.AddDbContext<CarAPIEmployeeContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("CarAPIEmployeeContext") ?? throw new InvalidOperationException("Connection string 'CarAPIEmployeeContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
