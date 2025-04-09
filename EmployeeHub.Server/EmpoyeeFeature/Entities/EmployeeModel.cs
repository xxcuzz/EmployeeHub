namespace EmployeeHub.Server.EmpoyeeFeature.Entities;

public class EmployeeModel
{
    public EmployeeModel()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }

    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public DateTime? Age { get; set; }

    public required Genders Gender { get; init; }
}
