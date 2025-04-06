using EmployeeHub.Server.EmployeeContract;
using EmployeeHub.Server.EmployeeContract.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeHub.Server.EmpoyeeFeature.Endpoints;

public class GetEmployeeEndpoint : EndpointWithoutRequest<EmployeeResponse>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeEndpoint(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override void Configure()
    {
        Get("/employees/{id}");
        Description(x => x
            .WithName("Get Employee by ID")
            .Produces<EmployeeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
        );
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");
        if (id == null || !Guid.TryParse(id, out var guid))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var employee = await _employeeRepository.GetEmployeeByIdAsync(guid, ct);

        if (employee == null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(employee.MapToResponse(), ct);
    }
}