namespace EmployeeHub.Server.EmpoyeeFeature.Entities;

public class EmployeeModel
{
    public Guid Id { get; set; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public DateTime? Age { get; set; }

    public required Genders Gender { get; init; }
}

public static class EmployeeModelExtensions
{
    public static int? GetAge(this EmployeeModel employee)
    {
        if (employee.Age == null)
        {
            return null;
        }

        var dateOfBirth = employee.Age.Value;

        var age = DateTime.Today.Year - dateOfBirth.Year;

        return DateTime.Today.Date > dateOfBirth.Date ? age : age - 1;
    }
}