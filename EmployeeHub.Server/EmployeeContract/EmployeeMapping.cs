using EmployeeHub.Server.EmployeeContract.Requests;
using EmployeeHub.Server.EmployeeContract.Responses;
using EmployeeHub.Server.EmpoyeeFeature.Entities;

namespace EmployeeHub.Server.EmployeeContract;

public static class EmployeeMapping
{
    public static EmployeeModel MapToModel(this EmployeeRequest employeeRequest)
    {
        if(!Enum.TryParse<Genders>(employeeRequest.Gender, out var gender))
        {
            throw new ArgumentException($"Invalid gender value: {employeeRequest.Gender}");
        }

        return new EmployeeModel
        {
            Id = Guid.NewGuid(),
            FirstName = employeeRequest.FirstName,
            LastName = employeeRequest.LastName,
            Age = employeeRequest.Age,
            Gender = gender,
        };
    }

    public static EmployeeResponse MapToResponse(this EmployeeModel employeeModel)
    {
        return new EmployeeResponse(
            employeeModel.Id,
            $"{employeeModel.FirstName} {employeeModel.LastName}",
            employeeModel.GetAge(),
            employeeModel.Gender.ToString()
        );
    }

    public static EmployeesResponse MapToResponse(this IEnumerable<EmployeeModel> employees)
    {
        return new EmployeesResponse(employees.Select(MapToResponse));
    }
}
