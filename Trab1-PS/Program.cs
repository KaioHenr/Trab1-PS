using Microsoft.EntityFrameworkCore;
using Trab1_PS.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurando o Entity Framework Core para usar PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();