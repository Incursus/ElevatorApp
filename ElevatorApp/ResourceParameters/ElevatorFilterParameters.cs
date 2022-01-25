using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ElevatorApp.ResourceParameters;

public class ElevatorFilterParameters
{
    [BindRequired]
    public int ElevatorId { get; set; }
    [BindRequired]
    public int DestinationFloor { get; set; }
}
