using ElevatorApp.ResourceParameters;
using FluentValidation;

namespace ElevatorApp.Validators;

public class ElevatorsFilterParametersValidator : AbstractValidator<ElevatorFilterParameters>
{
    public ElevatorsFilterParametersValidator()
    {
        this.RuleFor(rule => rule.ElevatorId).Must(BeExistingElevator).When(m => true).WithMessage("Elevator doesn't exist.");;
        this.RuleFor(rule => rule.CurrentFloor).NotEqual(rule => rule.DestinationFloor).WithMessage("Please choose a different floor than the one you're currently on.");
        this.RuleFor(rule => rule.CurrentFloor).Must(BeExistingFloor).When(m => true).WithMessage("Floor doesn't exist.");
        this.RuleFor(rule => rule.DestinationFloor).Must(BeExistingFloor).When(m => true).WithMessage("Floor doesn't exist.");
    }

    private bool BeExistingFloor(int floor) => floor is <= Constants.NumberOfFloors and >= 0;
    
    private bool BeExistingElevator(int elevatorId) => elevatorId is <= Constants.NumberOfElevators and >= 0;
}
