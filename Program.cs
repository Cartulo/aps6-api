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

var politicaCors = "_Aps6policy";
services.AddCors(o =>
    o.AddPolicy(name: politicaCors, builder =>
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

app.UseStaticFiles();

app.UseRouting();

app.UseCors(politicaCors);

app.UseAuthorization();

app.MapControllers();

app.Run();