namespace EmployeeHub.Server.EmployeeContract.Responses;

public record EmployeesResponse(IEnumerable<EmployeeResponse> Items);