using Aps6Api;
using Aps6Api.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<APS6DatabaseSettings>(builder.Configuration.GetSection("APS6Database"));

services.AddSingleton<ProdutosService>();
services.AddSingleton<MovimentacoesService>();
services.AddSingleton<SetoresService>();

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddCors(o =>
    o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins("http://localhost.com:7070")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

services.AddControllers();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();