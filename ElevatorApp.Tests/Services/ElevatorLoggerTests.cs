using ElevatorApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace ElevatorApp.Tests;

[TestClass]
public class ElevatorLoggerTests
{
    private ElevatorLogger elevatorLogger;

    [TestInitialize]
    public void Initialize()
    {
        elevatorLogger = new ElevatorLogger();
    }
    
    [TestMethod]
    public void GetAll_WhenCalled_ShouldPopulateDataCorrectly()
    {
        var elevator = new Elevator
        {
            ElevatorId = 1,
        };
        elevatorLogger.Add(elevator);
        

        var items= elevatorLogger.GetAll();

        items.Should().NotBeEmpty();
        items[0].ElevatorId.Should().Be(1);
    }
}
