namespace EmployeeHub.Server.EmployeeContract.Responses;

public record EmployeeResponse(Guid Id, string FirstName, string LastName, DateTime? Age, string Gender);
