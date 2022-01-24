using System.Text.Json.Serialization;
using ElevatorApp.Interfaces;
using ElevatorApp.Model;
using ElevatorApp.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Thread = ElevatorApp.Interfaces.Thread;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status415UnsupportedMediaType));
    options.ReturnHttpNotAcceptable = true;
});

builder.Services.AddMvc(_ => { })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ElevatorsFilterParametersValidator>())
    .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

builder.Services.AddTransient<IThread, Thread>();
builder.Services.AddTransient<IElevatorEngine, ElevatorEngine>();

var app = builder.Build();

app.MapGet("/", () => "Elevator App!");

app.UseRouting();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();