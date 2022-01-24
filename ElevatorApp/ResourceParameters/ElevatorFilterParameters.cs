using System.ComponentModel.DataAnnotations;
using ElevatorApp.Model;

namespace ElevatorApp.ResourceParameters;

public class ElevatorFilterParameters
{
    public int ElevatorId { get; set; }
    
    public int DestinationFloor { get; set; }
}
