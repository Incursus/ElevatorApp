using System.Diagnostics.CodeAnalysis;
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
    [DynamicData(nameof(GetTestData), DynamicDataSourceType.Method)]
    public void ElevatorsFilterParametersValidator_ProvidedFilter_ShouldValidateCorrectly(ElevatorFilterParameters filter, bool expected)
    {
        var validator = new ElevatorsFilterParametersValidator();

        var validationResult = validator.TestValidateAsync(filter);

        validationResult.Result.IsValid.Should().Be(expected);
    }
    
    private static IEnumerable<object[]> GetTestData()
    {
        yield return new object[] { new ElevatorFilterParameters() {ElevatorId = 999} };
    }
}