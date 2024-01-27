using FluentValidation;

namespace CarTrader.Services.Cars.Application.Commands.AddCar
{
    public class AddCarCommandValidator : AbstractValidator<AddCarCommand>
    {
        public AddCarCommandValidator()
        {
            RuleFor(x => x.Car.Manfacturer)
                .NotEmpty()
                .WithMessage("Car Manfacturer cannot be empty");
            RuleFor(x => x.Car.Model)
                .NotEmpty()
                .WithMessage("Car Model cannot be empty");
        }
    }
}
