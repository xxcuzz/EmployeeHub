using EmployeeHub.Server.EmployeeContract;
using EmployeeHub.Server.EmployeeContract.Requests;
using EmployeeHub.Server.EmployeeContract.Responses;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EmployeeHub.Server.EmpoyeeFeature.Endpoints;

public class CreateEmployeeEndpoint : 
    Endpoint<EmployeeRequest, Results<Created<EmployeeResponse>, BadRequest>>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeEndpoint(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public override void Configure()
    {
        Post("/employees");
        Description(x => x
            .WithName("Add Employee")
            .Produces<EmployeeResponse>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest));
    }

    public override async Task<Results<Created<EmployeeResponse>, BadRequest>> ExecuteAsync(
        EmployeeRequest req, CancellationToken ct)
    {
        var employee = req.MapToModel();
       
        var affectedRows = await _employeeRepository.CreateEmployeeAsync(employee, ct);

        if (affectedRows > 0)
        {
            return TypedResults.Created($"/employees/{employee.Id}", employee.MapToResponse());
        }

        return TypedResults.BadRequest();
    }
}
