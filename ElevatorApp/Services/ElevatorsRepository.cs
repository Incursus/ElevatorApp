using ElevatorApp.Model;

namespace ElevatorApp.Services;

public class ElevatorsRepository
{
    private List<Elevator> elevators = new();
    private List<ElevatorLogs> elevatorLogs = new();

    public ElevatorsRepository()
    {
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
        
        elevator.MovementState = this.GetMovementState(destinationFloor, elevator);

        if (elevator.MovementState == MovementState.Idle)
        {
            return;
        }
        
        if (elevator.MovementState is MovementState.Up or MovementState.Down)
        {
            CloseDoors(elevator);

            var numberOfFloorsToMove = Math.Abs(elevator.CurrentFloor - destinationFloor);

            for (int i = 0; i < numberOfFloorsToMove; i++)
            {
                Thread.Sleep(1000);

                if (elevator.MovementState == MovementState.Up)
                {
                    elevator.CurrentFloor++;
                }
                else if (elevator.MovementState == MovementState.Down)
                {
                    elevator.CurrentFloor--;
                }
            }

            elevator.MovementState = MovementState.Idle;
            elevator.CurrentFloor = destinationFloor;

            OpenDoors(elevator);
        }
    }

    private MovementState GetMovementState(int destinationFloor, Elevator elevator)
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
    
    public void CloseDoors(Elevator elevator)
    {
        // TODO Logic to determine if doors already closed
        
        Thread.Sleep(2000);
        elevator.DoorState = DoorState.Closed;
    }
    
    public void OpenDoors(Elevator elevator)
    {
        // TODO Logic to determine if doors already opened

        Thread.Sleep(2000);
        elevator.DoorState = DoorState.Opened;
    }
}

internal class ElevatorLogs
{
}
