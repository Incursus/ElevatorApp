using ElevatorApp.Model;

namespace ElevatorApp.Model;

public static class ElevatorLogger
{
    private static List<ElevatorEvent> entries = new();

    public static void Add(Elevator elevator)
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

    public static List<ElevatorEvent> GetAll()
    {
        return entries;
    }

    public static ElevatorEvent Get(int eventId)
    {
        // TODO Use Dictionary lookup for better performance
        return entries.FirstOrDefault(x => x.Id == eventId);
    }
}
