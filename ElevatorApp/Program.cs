using ElevatorApp.Services;
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

builder.Services.AddSingleton<ElevatorsRepository>();

var app = builder.Build();

app.MapGet("/", () => "Elevator App!");

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();