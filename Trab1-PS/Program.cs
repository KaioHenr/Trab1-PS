using Microsoft.EntityFrameworkCore;
using Trab1_PS.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

// Configurando o Entity Framework Core com InMemory
// O banco de dados "AppDbContext" será armazenado na memória enquanto a aplicação estiver em execução
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("AppDbContext"));

// Permite que a aplicação processe requisições HTTP que são para os controladores
builder.Services.AddControllers();

// Configurando o suporte a sessões de login
// Usado para armazenar informações temporárias, como login do usuário
builder.Services.AddDistributedMemoryCache(); // Cache para gerenciar dados de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Tempo de expiração da sessão
});

var app = builder.Build(); 



// Mapeia os endpoints configurados nos controladores
app.MapControllers();

app.Run();