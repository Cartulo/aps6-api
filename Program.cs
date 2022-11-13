using Microsoft.EntityFrameworkCore;
using Aps6Api.Produtos.Contexts;
using Aps6Api.Movimentacoes.Contexts;
using Aps6Api.Setores.Contexts;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// services.AddEndpointsApiExplorer();

// var StringConexao = "Data Source=localhost;Initial Catalog=Aps6BD;Integrated Security=True";
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// builder.Services.AddSwaggerAps6();
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//                       builder =>
//                       {
//                           builder.AllowAnyHeader()
//                                  .AllowAnyMethod()
//                                  .AllowAnyOrigin();
//                       });
// });

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovimentacoesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=localhost;Initial Catalog=Aps6BD;Integrated Security=True"));
});

builder.Services.AddDbContext<ProdutosContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=localhost;Initial Catalog=Aps6BD;Integrated Security=True"));
});

builder.Services.AddDbContext<SetoresContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data Source=localhost;Initial Catalog=Aps6BD;Integrated Security=True"));
});
// builder.Services.AddSwaggerGen(c =>
// {
//    c.SwaggerDoc("v1", new() { Title = "Aps6Api", Version = "v1" });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();