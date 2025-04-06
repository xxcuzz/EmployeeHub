namespace EmployeeHub.Server.EmployeeContract.Requests;

public record DeleteEmployeesRequest(List<Guid> Ids);