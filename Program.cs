using Microsoft.EntityFrameworkCore;
using SpecificationPatternInEfCore.Persistent;
using SpecificationPatternInEfCore.Service;

namespace SpecificationPatternInEfCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        builder.Services.AddTransient<IGameService, GameService>();
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


        using var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        SeedData.SeedRecords(context);

        app.Run();
    }
}
