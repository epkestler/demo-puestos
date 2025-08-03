using ApiEmpleados.Models;
using ApiEmpleados.Services;
using ApiEmpleados.Util;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBaseService<Puesto>, PuestoService>();
builder.Services.AddScoped<IBaseService<Empleado>, EmpleadoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionHandler(config =>
{
    config.Run(async context =>
    {
        var arg = context.Features.Get<IExceptionHandlerPathFeature>();
        context.Response.ContentType = "application/json";

        if (arg?.Error is BusinessLogicException) {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(arg.Error.Message);
        }
        else
        {
            context.Response.StatusCode = 500;
            File.AppendAllText("logs.log", $"{DateTime.Now} {context.TraceIdentifier} - {arg?.Endpoint.ToString()} - {arg?.Error.ToString()} ");
            
            await context.Response.WriteAsync($"Ocurrió un error inesperado. Por favor, intente más tarde. bodt. {context.TraceIdentifier}");
        }
            
    });
});

app.MapControllers();

app.Run();
