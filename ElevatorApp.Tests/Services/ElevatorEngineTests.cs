using ElevatorApp.Interfaces;
using ElevatorApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevatorApp.Tests;

[TestClass]
public class ElevatorEngineTests
{
    private ElevatorEngine elevatorEngine;
    private Mock<IThread> threadMock;

    [TestInitialize]
    public void Initialize()
    {
        threadMock = new Mock<IThread>();
        elevatorEngine = new ElevatorEngine(threadMock.Object, new Mock<IElevatorLogger>().Object);
    }
    
    [TestMethod]
    public void Move_WhenCalled_ShouldProperlyDelay()
    {
        elevatorEngine.Move(0, 1);

        threadMock.Verify(x => x.Sleep(It.IsAny<int>()), Times.AtLeastOnce);
    }
}
