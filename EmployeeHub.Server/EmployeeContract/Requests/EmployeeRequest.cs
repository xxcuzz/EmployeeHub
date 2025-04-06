namespace EmployeeHub.Server.EmployeeContract.Requests;

public record EmployeeRequest(string FirstName, string LastName, DateTime? Age, string Gender);
