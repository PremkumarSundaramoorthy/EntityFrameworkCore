
using EmployeePortal.Application.Common;
using EmployeePortal.DataAccess.Common;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace EmployeePortal.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region DataBase

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            static async void UpdateSeedDataAsync(IHost host)
            {
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    try
                    {
                        var context = services.GetRequiredService<ApplicationDbContext>();

                        if (context.Database.IsSqlServer())
                        {
                            context.Database.Migrate();
                        }

                        await SeedData.SeedDataAsync(context);
                    }
                    catch (Exception)
                    {
                        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                        logger.LogError("Database connectivity failed");
                    }
                }
            }

            var app = builder.Build();

            UpdateSeedDataAsync(app);

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