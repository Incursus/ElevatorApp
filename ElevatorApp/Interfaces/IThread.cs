namespace ElevatorApp.Interfaces;

public interface IThread
{
    void Sleep(int milliseconds);
}

public class Thread : IThread
{
    public void Sleep(int milliseconds)
    {
        System.Threading.Thread.Sleep(milliseconds);
    }
}
