using System.Net.Mime;
using ElevatorApp.Model;
using ElevatorApp.ResourceParameters;
using ElevatorApp.ResultFilters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ElevatorApp.Controllers;

[ApiController]
[Route("api/elevators")]
[Produces(MediaTypeNames.Application.Json)]
public class ElevatorsController : ControllerBase
{
    private const string GetFloorsRoute = nameof(GetFloorsRoute);

    private readonly ILogger<ElevatorsController> logger;

    private List<Elevator> elevator;

    public ElevatorsController(ILogger<ElevatorsController> logger, List<Elevator> elevator)
    {
        this.logger = logger;
        this.elevator = elevator;
    }

    [HttpGet(Name = GetFloorsRoute)]
    [FloorsResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Elevator))]
    public async Task<ActionResult> CallElevator([FromQuery] ElevatorFilterParameters queryStringParameters)
    {
        this.logger.LogInformation(
            "[{ClassName}] Request with parameters: {RequestParameters}",
            nameof(this.CallElevator),
            JsonConvert.SerializeObject(queryStringParameters));

        var startTime = DateTime.UtcNow;
        
        for (int i = 0; i < Constants.NumberOfElevators; i++)
        {
            elevator.Add(new Elevator
            {
                // TODO have floor in memory rather than from user query. Start from floor 0
                ElevatorId = i,
                CurrentFloor = queryStringParameters.CurrentFloor,
                DestinationFloor = queryStringParameters.DestinationFloor
            });
        }

        // TODO: Move DetermineState method
        elevator[queryStringParameters.ElevatorId].State = elevator[queryStringParameters.ElevatorId].DetermineState(queryStringParameters.DestinationFloor);
        
        // Imitate elevator door open/close on entering and exiting elevator and total travel time
        Thread.Sleep(Math.Abs(elevator[queryStringParameters.ElevatorId].CurrentFloor - queryStringParameters.DestinationFloor) * 1000 + 4000);

        // TODO: implement logic of real time elevator. Each second, add or move the floor of the elevator
        var operationTimeInSeconds = Math.Abs(elevator[queryStringParameters.ElevatorId].CurrentFloor - queryStringParameters.DestinationFloor) + 4;

        var response = new Response()
        {
            Elevator = new Elevator
            {
                ElevatorId = queryStringParameters.ElevatorId, CurrentFloor = queryStringParameters.CurrentFloor,
                DestinationFloor = queryStringParameters.DestinationFloor
            },
            OperationTimeInSeconds = operationTimeInSeconds
        };

        elevator[queryStringParameters.ElevatorId].SetCurrentFloor(queryStringParameters.DestinationFloor);

        this.logger.LogInformation(
            "[{ClassName}] Return. Duration {Elapsed} ms",
            nameof(this.CallElevator),
            DateTime.UtcNow.Subtract(startTime).TotalMilliseconds);

        return this.Ok(response);
    }

    public async Task<ActionResult> GetElevatorInfo([FromQuery] int elevatorId)
    {
        // TODO Implement
    }

}

public class Response
{
    public Elevator Elevator { get; set; }

    public double OperationTimeInSeconds { get; set; }
}
