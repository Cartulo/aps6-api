using Microsoft.EntityFrameworkCore;
using Aps6Api.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<Aps6Context>(opt =>
    opt.UseInMemoryDatabase("Aps6List"));
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