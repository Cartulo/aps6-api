using Microsoft.EntityFrameworkCore;
using Aps6Api.Produtos.Contexts;
using Aps6Api.Movimentacoes.Contexts;
using Aps6Api.Setores.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MovimentacoesContext>(opt =>
    opt.UseInMemoryDatabase("MovimentacoesList"));
builder.Services.AddDbContext<ProdutosContext>(opt =>
    opt.UseInMemoryDatabase("ProdutosList"));
builder.Services.AddDbContext<SetoresContext>(opt =>
    opt.UseInMemoryDatabase("SetoresList"));
// builder.Services.AddSwaggerGen(c =>
// {
//    c.SwaggerDoc("v1", new() { Title = "Aps6Api", Version = "v1" });
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aps6Api v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();