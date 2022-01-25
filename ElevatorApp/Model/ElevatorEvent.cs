namespace ElevatorApp.Model;

public class ElevatorEvent
{
    public int Id { get; set; }
    
    public int ElevatorId { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public DoorState DoorState { get; set; }

    public MovementState MovementState { get; set; }
}
