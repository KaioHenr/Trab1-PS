using Trab1_PS.Data; // Para o AppDbContext
using Trab1_PS.Repository; // Para as implementações dos repositórios
using Trab1_PS.Repository.Interfaces; // Para as interfaces dos repositórios
using Trab1_PS.Services; // Para as implementações dos serviços
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos controladores
builder.Services.AddControllers();

// Configuração do Swagger para documentação da API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp",
        builder => builder.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

// Configuração do Entity Framework com banco de dados InMemory
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase(databaseName: "AppDbContext"),
    ServiceLifetime.Scoped
);

// Registro dos repositórios no container de DI
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<IDoramaRepository, DoramaRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

// Registro dos serviços no container de DI
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAvaliacaoService, AvaliacaoService>();
builder.Services.AddScoped<IDoramaService, DoramaService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();

var app = builder.Build();

// Configuração para ambientes de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuração padrão do pipeline
app.UseCors("AllowVueApp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();