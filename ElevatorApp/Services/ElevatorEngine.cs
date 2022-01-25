using ElevatorApp.Interfaces;
using ElevatorApp.Model;

namespace ElevatorApp.Services;

public class ElevatorEngine : IElevatorEngine
{
    private readonly IThread thread;
    private readonly IElevatorLogger elevatorLogger;
    private readonly List<Elevator> elevators = new();

    public ElevatorEngine(IThread thread, IElevatorLogger elevatorLogger)
    {
        this.thread = thread;
        this.elevatorLogger = elevatorLogger;

        for (int i = 0; i < Constants.NumberOfElevators; i++)
        {
            elevators.Add(new Elevator
            {
                ElevatorId = i,
                CurrentFloor = 0,
                DoorState = DoorState.Closed,
                MovementState = MovementState.Idle
            });
        }
    }

    public Elevator Get(int elevatorId)
    {
        return elevators[elevatorId];
    }

    public void Move(int elevatorId, int destinationFloor)
    {
        var elevator = elevators[elevatorId];
        
        elevator.MovementState = GetMovementState(destinationFloor, elevator);

        elevatorLogger.Add(elevator);

        if (elevator.MovementState == MovementState.Idle)
        {
            return;
        }
        
        if (elevator.MovementState is MovementState.Up or MovementState.Down)
        {
            CloseDoors(elevator);
            elevatorLogger.Add(elevator);

            var numberOfFloorsToMove = Math.Abs(elevator.CurrentFloor - destinationFloor);

            for (var i = 0; i < numberOfFloorsToMove; i++)
            {
                thread.Sleep(1000);

                if (elevator.MovementState == MovementState.Up)
                {
                    elevator.CurrentFloor++;
                    elevatorLogger.Add(elevator);
                }
                else if (elevator.MovementState == MovementState.Down)
                {
                    elevator.CurrentFloor--;
                    elevatorLogger.Add(elevator);
                }
            }

            elevator.MovementState = MovementState.Idle;
            elevator.CurrentFloor = destinationFloor;

            OpenDoors(elevator);
            elevatorLogger.Add(elevator);
        }
    }

    private static MovementState GetMovementState(int destinationFloor, Elevator elevator)
    {
        if (destinationFloor < elevator.CurrentFloor)
        {
            return MovementState.Down;
        }

        if (destinationFloor > elevator.CurrentFloor)
        {
            return MovementState.Up;
        }

        return MovementState.Idle;
    }

    private void CloseDoors(Elevator elevator)
    {
        // TODO Logic to determine if doors already closed
        
        thread.Sleep(2000);
        elevator.DoorState = DoorState.Closed;
    }

    private void OpenDoors(Elevator elevator)
    {
        // TODO Logic to determine if doors already opened

        thread.Sleep(2000);
        elevator.DoorState = DoorState.Opened;
    }
}
