using ElevatorApp.ResourceParameters;
using FluentValidation;

namespace ElevatorApp.Validators;

public class ElevatorsFilterParametersValidator : AbstractValidator<ElevatorFilterParameters>
{
    public ElevatorsFilterParametersValidator()
    {
        this.RuleFor(rule => rule.ElevatorId).Must(BeExistingElevator).When(_ => true).WithMessage("Elevator doesn't exist.");
        this.RuleFor(rule => rule.DestinationFloor).Must(BeExistingFloor).When(_ => true).WithMessage("Floor doesn't exist.");
    }

    private bool BeExistingFloor(int floor) => floor is <= Constants.NumberOfFloors and >= 0;
    
    private bool BeExistingElevator(int elevatorId) => elevatorId is <= Constants.NumberOfElevators and >= 0;
}
