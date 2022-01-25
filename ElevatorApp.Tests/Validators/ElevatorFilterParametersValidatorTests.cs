using ElevatorApp.ResourceParameters;
using ElevatorApp.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElevatorApp.Tests.Validators;

[TestClass]
public class ElevatorFilterParametersValidatorTests
{
    [TestMethod]
    public void ElevatorsFilterParametersValidator_ProvidedIncorrectFilter_ShouldValidateToFalse()
    {
        var validator = new ElevatorsFilterParametersValidator();

        var validationResult = validator.TestValidateAsync(new ElevatorFilterParameters() {ElevatorId = -1, DestinationFloor = -1});

        validationResult.Result.IsValid.Should().Be(false);
    }
    
    [TestMethod]
    public void ElevatorsFilterParametersValidator_ProvidedCorrectFilter_ShouldValidateToTrue()
    {
        var validator = new ElevatorsFilterParametersValidator();

        var validationResult = validator.TestValidateAsync(new ElevatorFilterParameters() {ElevatorId = Constants.NumberOfElevators, DestinationFloor = Constants.NumberOfFloors});

        validationResult.Result.IsValid.Should().Be(true);
    }
}
