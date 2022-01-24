namespace ElevatorApp.Model;

public class Elevator
{
    public int ElevatorId { get; set; }

    public MovementState MovementState { get; set; }
    
    public DoorState DoorState { get; set; }
    
    public int CurrentFloor { get; set; }
}