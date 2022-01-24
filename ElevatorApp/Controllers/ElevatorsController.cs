using System.Net.Mime;
using ElevatorApp.Model;
using ElevatorApp.ResourceParameters;
using ElevatorApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ElevatorApp.Controllers;

[Produces(MediaTypeNames.Application.Json)]
public class ElevatorsController : ControllerBase
{
    
    private readonly ILogger<ElevatorsController> logger;
    private readonly ElevatorsRepository repository;

    public ElevatorsController(ILogger<ElevatorsController> logger, ElevatorsRepository repository)
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
    public async Task<ActionResult> GetInfo([FromQuery] int elevatorId)
    {
        return this.Ok(repository.Get(elevatorId));
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Elevator))]
    public async Task<ActionResult> GetLogs([FromQuery] int elevatorId)
    {
        return this.Ok(repository.Get(elevatorId));
    }
}
