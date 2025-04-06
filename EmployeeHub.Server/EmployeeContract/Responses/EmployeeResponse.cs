namespace EmployeeHub.Server.EmployeeContract.Responses;

public record EmployeeResponse(Guid Id, string FullName, int? Age, string Gender);
