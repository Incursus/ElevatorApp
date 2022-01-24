using System.Data;

namespace ElevatorApp.Model;

public class Elevator
{
    public int ElevatorId { get; set; }

    public MovementState MovementState { get; set; }
    
    public DoorState DoorState { get; set; }
    
    public int CurrentFloor { get; set; }
}

public enum DoorState
{
    Closed,
    Opened
}

public enum MovementState
{
    Up,
    Down,
    Idle
}
