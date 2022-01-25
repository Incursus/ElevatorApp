using ElevatorApp.Interfaces;

namespace ElevatorApp.Model;

public class ElevatorLogger : IElevatorLogger
{
    private readonly List<ElevatorEvent> entries = new();

    public void Add(Elevator elevator)
    {
        entries.Add(new ElevatorEvent
        {
            Id = entries.Count,
            ElevatorId = elevator.ElevatorId,
            CreatedAt = DateTime.UtcNow,
            DoorState = elevator.DoorState,
            MovementState = elevator.MovementState
        });
    }

    public List<ElevatorEvent> GetAll()
    {
        return entries;
    }

    public ElevatorEvent Get(int eventId)
    {
        // TODO Use Dictionary lookup for better performance
        return entries.FirstOrDefault(x => x.Id == eventId);
    }
}
