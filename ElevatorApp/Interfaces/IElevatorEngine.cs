using ElevatorApp.Model;

namespace ElevatorApp.Interfaces;

public interface IElevatorEngine
{
    Elevator Get(int elevatorId);
    void Move(int elevatorId, int destinationFloor);
}
