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
            .WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

        //RuleFor(x => x.Age)
        //    .InclusiveBetween(18, 100)
        //    .WithMessage("Age must be between 18 and 100.");

        RuleFor(x => x.Gender)
            .NotEmpty()
            .WithMessage("Gender is required.")
            .Must(i => Enum.IsDefined(typeof(Genders), i))
            .WithMessage("Invalid gender");
    }
}
