using Trab1_PS.Data; // Para o AppDbContext
using Trab1_PS.Repository; // Para as implementações dos repositórios
using Trab1_PS.Repository.Interfaces; // Para as interfaces dos repositórios
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos controladores
builder.Services.AddControllers();

// Configuração do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do Entity Framework com banco de dados InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase(databaseName: "AppDbContext"),
    ServiceLifetime.Scoped
);

// Registro dos repositórios no container de DI
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<IDoramaRepository, DoramaRepository>();

var app = builder.Build();

// Configuração para ambientes de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração padrão do pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();