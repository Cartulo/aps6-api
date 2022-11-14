using Aps6Api;
using Aps6Api.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.Configure<APS6DatabaseSettings>(
    builder.Configuration.GetSection("APS6Database"));

services.AddSingleton<ProdutosService>();
services.AddSingleton<MovimentacoesService>();
services.AddSingleton<SetoresService>();
// services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
// builder.Services.AddSwaggerAps6();
services.AddCors(o => 
    o.AddPolicy("MyPolicy", builder =>
        {
            builder.WithOrigins("http://localhost.com:7070")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));

// Add services to the container.

services.AddControllers();
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
app.UseRouting();
// app.UseStaticFiles();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();