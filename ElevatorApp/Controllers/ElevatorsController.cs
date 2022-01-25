using System.Net.Mime;
using ElevatorApp.Interfaces;
using ElevatorApp.Model;
using ElevatorApp.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace ElevatorApp.Controllers;

[ApiController]
[Route("api/elevators")]
[Produces(MediaTypeNames.Application.Json)]
public class ElevatorsController : ControllerBase
{
    private readonly IElevatorEngine repository;
    private readonly IElevatorLogger elevatorLogger;

    public ElevatorsController(IElevatorEngine repository, IElevatorLogger elevatorLogger)
    {
        this.repository = repository;
        this.elevatorLogger = elevatorLogger;
    }
    
    [HttpGet(nameof(Call))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Elevator))]
    public async Task<ActionResult> Call([FromQuery] ElevatorFilterParameters queryStringParameters)
    {
        repository.Move(queryStringParameters.ElevatorId, queryStringParameters.DestinationFloor);

        return await Task.FromResult<ActionResult>(this.Ok());
    }

    [HttpGet(nameof(GetInfo))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Elevator))]
    public async Task<ActionResult> GetInfo([FromQuery] int elevatorId)
    {
        return await Task.FromResult<ActionResult>(this.Ok(repository.Get(elevatorId)));
    }
    
    [HttpGet(nameof(GetLogs))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ElevatorEvent>))]
    public async Task<ActionResult> GetLogs()
    {
        return await Task.FromResult<ActionResult>(this.Ok(elevatorLogger.GetAll()));
    }
    
    [HttpGet(nameof(GetLog))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ElevatorEvent))]
    public async Task<ActionResult> GetLog([FromQuery] int eventId)
    {
        return await Task.FromResult<ActionResult>(this.Ok(elevatorLogger.Get(eventId)));
    }
}
