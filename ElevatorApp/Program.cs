using ElevatorApp.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status415UnsupportedMediaType));
    options.ReturnHttpNotAcceptable = true;
});

builder.Services.AddMvc(setup => { }).AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<ElevatorsFilterParametersValidator>());


var app = builder.Build();

app.MapGet("/", () => "Elevator App!");

app.UseRouting();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();