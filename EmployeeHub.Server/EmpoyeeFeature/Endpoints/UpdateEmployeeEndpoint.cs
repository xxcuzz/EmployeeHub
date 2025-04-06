using EmployeeHub.Server.EmployeeContract;
using EmployeeHub.Server.EmployeeContract.Requests;
using EmployeeHub.Server.EmployeeContract.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeHub.Server.EmpoyeeFeature.Endpoints;

public class UpdateEmployeeEndpoint : 
    Endpoint<EmployeeRequest, Results<Ok, NotFound, BadRequest>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeEndpoint(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override void Configure()
    {
        Put("/employees/{id}");
        Description(x => x
            .WithName("Update Employee")
            .Produces<EmployeeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest));
    }

    public override async Task<Results<Ok, NotFound, BadRequest>> ExecuteAsync(EmployeeRequest req, CancellationToken ct)
    {
        var id = Route<string>("id");
        if (id == null || !Guid.TryParse(id, out var guid))
        {
            return TypedResults.NotFound();
        }
        
        var employee = req.MapToModel();
        employee.Id = guid;

        var affectedRows = await _employeeRepository.UpdateEmployeeAsync(employee, ct);

        if (affectedRows > 0)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.BadRequest();
        }
    }
}
