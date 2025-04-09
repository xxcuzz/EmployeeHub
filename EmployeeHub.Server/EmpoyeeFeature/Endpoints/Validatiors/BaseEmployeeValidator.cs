using EmployeeHub.Server.EmployeeContract.Requests;
using EmployeeHub.Server.EmpoyeeFeature.Entities;
using FastEndpoints;
using FluentValidation;

namespace EmployeeHub.Server.EmpoyeeFeature.Endpoints.Validatiors;


public class BaseEmployeeValidator : Validator<EmployeeRequest>
{
    public BaseEmployeeValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .MinimumLength(2)
            .WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(20)
            .WithMessage("First name must be at most 20 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .MinimumLength(2)
            .WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(20)
            .WithMessage("First name must be at most 20 characters long.");

        RuleFor(x => x.Age)
            .Must(BeBetween18And100)
            .WithMessage("Age must be between 18 and 100.");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .WithMessage("Gender is required.")
            .Must(i => Enum.IsDefined(typeof(Genders), i))
            .WithMessage("Invalid gender");
    }

    private static bool BeBetween18And100(DateTime? age)
    {
        if (!age.HasValue)
        {
            return true;
        }

        int calculatedAge = CalculateAge(age.Value);
        return calculatedAge >= 18 && calculatedAge <= 100;
    }

    private static int CalculateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }
}
