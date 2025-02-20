using Microsoft.EntityFrameworkCore;
using ProfileAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProfileAPI.Entities;
using Serilog;
using Serilog.Events;
using ProfileAPI.Repositories.Implementation;
using ProfileAPI.Repositories.Contracts;
using ProfileAPI.Data;
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.


    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("Logs/logs.txt",
                  rollingInterval: RollingInterval.Day,
                  restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.File("Logs/errors-.txt",
                  rollingInterval: RollingInterval.Day,
                  restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

    builder.Host.UseSerilog();

    //Add DataBase
    builder.Services.AddDbContext<ProfileDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")??
            throw new InvalidOperationException("Sorry, your connection is not found..."));

    });

    builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();

    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy", policy =>
        {
            policy.AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowAnyOrigin();
        });
    });


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("CorsPolicy");
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "ProfileAPI zakoñczy³a siê z powodu  b³êdu.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
