// using Microsoft.EntityFrameworkCore;
// using Trab1_PS.Models;
//
// var builder = WebApplication.CreateBuilder(args);
//
// // Adiciona o DbContext e configura o banco de dados (SQLite no exemplo)
// // builder.Services.AddDbContext<AvaliacaoDb>(options =>
// //     options.UseSqlite("Data Source=avaliacao.db"));
// Usuario test = new Usuario(1,"kaio","test@","123",new List<Avaliacao>());
// Console.WriteLine(test);
// // Adiciona serviços para controllers (somente para API)
// builder.Services.AddControllers();
//
// var app = builder.Build();
//
// // Configuração do pipeline de requisições para a API
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     app.UseHsts();
// }
//
//
// app.UseHttpsRedirection();
// app.UseAuthorization();
//
// // Mapear os controladores da API
// app.MapControllers();
//
// app.Run();
var builder = WebApplication.CreateBuilder(args);

// Adicionando os serviços necessários para controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configurando a rota para os controladores
app.MapControllers();

app.Run();
