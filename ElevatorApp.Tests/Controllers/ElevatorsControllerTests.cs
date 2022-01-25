using ElevatorApp.Controllers;
using ElevatorApp.Interfaces;
using ElevatorApp.Model;
using ElevatorApp.ResourceParameters;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ElevatorApp.Tests.Controllers;

[TestClass]
public class ElevatorsControllerTests
{
    private ElevatorsController controller;
    private Mock<IElevatorEngine> engineMock;

    [TestInitialize]
    public void Initialize()
    {
        engineMock = new Mock<IElevatorEngine>();
        controller = new ElevatorsController(new Mock<ILogger<ElevatorsController>>().Object, engineMock.Object, new Mock<IElevatorLogger>().Object);
    }

    [TestMethod]
    public void Call_WhenCalled_ShouldMoveEngine()
    {
        controller.Call(new ElevatorFilterParameters
        {
            ElevatorId = 0,
            DestinationFloor = 1
        });

        engineMock.Verify(x => x.Move(0, 1), Times.Once);
    }
}
