using System.Net.Mime;
using ElevatorApp.Interfaces;
using ElevatorApp.Model;
using ElevatorApp.ResourceParameters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElevatorApp.Controllers;

[Produces(MediaTypeNames.Application.Json)]
public class ElevatorsController : ControllerBase
{
    
    private readonly ILogger<ElevatorsController> logger;
    private readonly IElevatorEngine repository;

    public ElevatorsController(ILogger<ElevatorsController> logger, IElevatorEngine repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Elevator))]
    public async Task<ActionResult> Call([FromQuery] ElevatorFilterParameters queryStringParameters)
    {
        this.logger.LogInformation(
            "[{ClassName}] Request with parameters: {RequestParameters}",
            nameof(this.Call),
            JsonConvert.SerializeObject(queryStringParameters));

        var startTime = DateTime.UtcNow;

        repository.Move(queryStringParameters.ElevatorId, queryStringParameters.DestinationFloor);
        
        this.logger.LogInformation(
            "[{ClassName}] Return. Duration {Elapsed} ms",
            nameof(this.Call),
            DateTime.UtcNow.Subtract(startTime).TotalMilliseconds);

        return await Task.FromResult<ActionResult>(this.Ok());
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Elevator))]
    public Task<ActionResult> GetInfo([FromQuery] int elevatorId)
    {
        return Task.FromResult<ActionResult>(this.Ok(repository.Get(elevatorId)));
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ElevatorEvent>))]
    public Task<ActionResult> GetLogs()
    {
        return Task.FromResult<ActionResult>(this.Ok(ElevatorLogger.GetAll()));
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ElevatorEvent))]
    public Task<ActionResult> GetLog([FromQuery] int eventId)
    {
        return Task.FromResult<ActionResult>(this.Ok(ElevatorLogger.Get(eventId)));
    }
}
