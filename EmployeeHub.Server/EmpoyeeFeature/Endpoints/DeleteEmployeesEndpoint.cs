using EmployeeHub.Server.EmployeeContract.Requests;
using EmployeeHub.Server.EmployeeContract.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeHub.Server.EmpoyeeFeature.Endpoints;

public class DeleteEmployeesEndpoint : Endpoint<DeleteEmployeesRequest, Results<Ok, NotFound>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployeesEndpoint(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }


    public override void Configure()
    {
        Delete("/employees");
        Description(x => x
            .WithName("Bulk Delete Employees")
            .Produces<EmployeeResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound));
    }

    public override async Task<Results<Ok, NotFound>> ExecuteAsync(DeleteEmployeesRequest req, CancellationToken ct)
    {
        var affectedRows = await _employeeRepository.DeleteEmployeesAsync(req.Ids, ct);
        if (affectedRows == req.Ids.Count)
        {
            return TypedResults.Ok();
        }
        else
        {
            return TypedResults.NotFound();
        }
    }
}
