using ElevatorApp.Model;

namespace ElevatorApp.ResourceParameters;

public class ElevatorFilterParameters
{
    public int ElevatorId { get; set; }
    
    public int DestinationFloor { get; set; }

    public int CurrentFloor { get; set; }
}
