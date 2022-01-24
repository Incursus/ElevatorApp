using System.Data;

namespace ElevatorApp.Model;

public class Elevator
{
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
    
    public void SetCurrentFloor(int destinationFloor)
    {
        CurrentFloor = destinationFloor;
    }
}

public class State
{
    public bool Up  { get; set; }
    
    public bool Down { get; set; }
    
    public bool Idle { get; set; }
}
