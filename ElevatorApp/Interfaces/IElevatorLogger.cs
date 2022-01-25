using ElevatorApp.Model;

namespace ElevatorApp.Interfaces;

public interface IElevatorLogger
{
     void Add(Elevator elevator);

     List<ElevatorEvent> GetAll();

     ElevatorEvent Get(int eventId);
}
