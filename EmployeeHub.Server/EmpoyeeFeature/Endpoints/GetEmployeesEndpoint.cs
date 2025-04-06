using EmployeeHub.Server.EmployeeContract;
using EmployeeHub.Server.EmployeeContract.Responses;
using FastEndpoints;

namespace EmployeeHub.Server.EmpoyeeFeature.Endpoints;

public class GetEmployeesEndpoint : EndpointWithoutRequest<EmployeesResponse>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesEndpoint(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override void Configure()
    {
        Get("/employees");
        Description(x => x
            .WithName("Get Employees")
            .Produces<EmployeesResponse>(StatusCodes.Status200OK));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _employeeRepository.GetAllEmployeesAsync(ct);

        await SendOkAsync(result.MapToResponse(), ct);
    }
}
