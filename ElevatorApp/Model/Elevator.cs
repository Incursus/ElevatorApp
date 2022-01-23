using System.Data;

namespace ElevatorApp.Model;

public class Elevator
{
    public Elevator(int destinationFloor)
    {
    }

    public int ElevatorId { get; set; }

    public string State { get; set; }

    public int CurrentFloor { get; set; }

    public int DestinationFloor { get; set; }
    
    public bool IsValid() => this.ElevatorId is >= Constants.NumberOfElevators and <= Constants.NumberOfElevators;

    public string DetermineState(int destinationFloor)
    {
        if (destinationFloor < CurrentFloor)
        {
            return "Down";
        }

        if (destinationFloor > CurrentFloor)
        {
            return "Up";
        }

        return "Idle";
    }
}

public class State
{
    public bool Up  { get; set; }
    
    public bool Down { get; set; }
    
    public bool Idle { get; set; }
}
