using System.Text.Json.Serialization;
using ElevatorApp.Interfaces;
using ElevatorApp.Model;
using ElevatorApp.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Thread = ElevatorApp.Interfaces.Thread;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status415UnsupportedMediaType));
    options.ReturnHttpNotAcceptable = true;
});

builder.Services.AddMvc(_ => { })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ElevatorsFilterParametersValidator>(includeInternalTypes: true))
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("v1", new OpenApiInfo {Title = "ElevatorApp", Version = "v1"});
});
    
builder.Services.AddSingleton<IThread, Thread>();
builder.Services.AddSingleton<IElevatorEngine, ElevatorEngine>();
builder.Services.AddSingleton<IElevatorLogger, ElevatorLogger>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ElevatorApp");
    c.RoutePrefix = string.Empty;
});

app.MapGet("/", () => "Elevator App!");

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();