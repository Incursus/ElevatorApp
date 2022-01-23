using System.Net.Mime;
using ElevatorApp.Model;
using ElevatorApp.ResourceParameters;
using ElevatorApp.ResultFilters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ElevatorApp.Controllers;

[ApiController]
[Route("api/floors")]
[Produces(MediaTypeNames.Application.Json)]
public class FloorsController : ControllerBase
{
    private const string GetFloorsRoute = nameof(GetFloorsRoute);
    
    private readonly ILogger<FloorsController> logger;
    
    public FloorsController(ILogger<FloorsController> logger)
    {
        this.logger = logger;
    }
    
    [HttpGet(Name = GetFloorsRoute)]
    [FloorsResultFilter]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Floor))]
    public async Task<ActionResult> CallElevator([FromQuery] ElevatorFilterParameters queryStringParameters)
    {
        this.logger.LogInformation(
            "[{ClassName}] Request with parameters: {RequestParameters}",
            nameof(this.CallElevator),
            JsonConvert.SerializeObject(queryStringParameters));
        
        var startTime = DateTime.UtcNow;

        // var floors = await this.dataProvider.GetData(filter);

        var elevator = new Elevator() { CurrentFloor = queryStringParameters.CurrentFloor, DestinationFloor = queryStringParameters.DestinationFloor };
        
        var floor = new Floor(queryStringParameters.DestinationFloor) {FloorNumber = queryStringParameters.DestinationFloor};

        elevator.State = elevator.DetermineState(queryStringParameters.DestinationFloor);
        
        var serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy(),
            },
        };
        
        var convertedToJson = JsonConvert.SerializeObject(elevator, Formatting.Indented, serializerSettings);

        var endTime =
            startTime.AddSeconds(Math.Abs(queryStringParameters.CurrentFloor - queryStringParameters.DestinationFloor) +
                                 2);
        this.logger.LogInformation(
            "[{ClassName}] Return. Duration {Elapsed} ms",
            nameof(this.CallElevator),
            DateTime.UtcNow.Subtract(startTime).TotalMilliseconds);

        return this.Ok(convertedToJson);
    }
}