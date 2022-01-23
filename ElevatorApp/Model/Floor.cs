namespace ElevatorApp.Model;

public class Floor
{
    public int FloorNumber { get; set; }
    
    public Floor(int floorNumber)
    {
        this.FloorNumber = floorNumber;
    }
    
    public bool IsValid() => this.FloorNumber is >= Constants.NumberOfFloors and <= Constants.NumberOfFloors;
}