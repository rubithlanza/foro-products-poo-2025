using Microsoft.EntityFrameworkCore;
using Productos.Api.DataBase.Entities;
using Productos.Api.Helpers;
using Productos.Api.Services;
using Productos.Api.Services.Interface;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//El mapa de migracion 
builder.Services.AddDbContext<ProductsDbContext>(options => options.UseSqlite(builder.Configuration
    .GetConnectionString("DefaultConnection")));

//Para el auto mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // Habilitar Scalar en la ruta /scalar/v1
    app.MapScalarApiReference("scalar/v1");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
