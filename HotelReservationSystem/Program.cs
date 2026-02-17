using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Data;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<HotelContext>(options =>
            options.UseInMemoryDatabase("HotelDb"));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod());
        });

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.InjectStylesheet("/swagger-custom.css");
            });
        }

        app.UseCors("AllowAll");

        app.MapControllers();

        app.Run();
    }
}