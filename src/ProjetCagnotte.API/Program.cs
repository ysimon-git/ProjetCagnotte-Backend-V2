using Microsoft.EntityFrameworkCore;
using ProjetCagnotte.API.Middlewares;
using ProjetCagnotte.Application;
using ProjetCagnotte.Infrastructure;
using ProjetCagnotte.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? new[] { "http://localhost:4200", "https://localhost:4200" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
               .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

Console.WriteLine($"Environment: {app.Environment.EnvironmentName}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("frontend");
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();