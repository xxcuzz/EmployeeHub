using EmployeeHub.Server.EmployeeContract.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeHub.Server.EmpoyeeFeature.Endpoints;

public class DeleteEmployeeEndpoint : 
    EndpointWithoutRequest<Results<Ok, NotFound>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeeEndpoint(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override void Configure()
    {
        Delete("/employees/{id}");
        Description(x => x
            .WithName("Delete Employee")
            .Produces<EmployeeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<string>("id");
        if (id == null || !Guid.TryParse(id, out var guid))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var affectedRows = await _employeeRepository.DeleteEmployeeAsync(guid, ct);

        if (affectedRows > 0)
        {
            await SendOkAsync(ct);
            return;
        }
        else
        {
            await SendErrorsAsync(400, ct);
            return;
        }
    }
}
