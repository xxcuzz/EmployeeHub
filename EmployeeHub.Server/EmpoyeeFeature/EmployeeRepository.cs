using Dapper;
using EmployeeHub.Server.DatabaseConfiguration;
using EmployeeHub.Server.EmpoyeeFeature.Entities;

namespace EmployeeHub.Server.EmpoyeeFeature;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDbConnectionFactory _db;

    public EmployeeRepository(IDbConnectionFactory db)
    {
        _db = db;
    }

    public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(CancellationToken ct)
    {
        using var connection = await _db.CreateConnectionAsync(ct);

        return await connection.QueryAsync<EmployeeModel>(
            """
                SELECT Id, FirstName, LastName, Age, Sex AS Gender
                FROM Employees
                """);
    }

    public async Task<int> CreateEmployeeAsync(EmployeeModel employee, CancellationToken ct)
    {
        using var connection = await _db.CreateConnectionAsync(ct);

        return await connection.ExecuteAsync(
                """
                 INSERT INTO Employees (Id, FirstName, LastName, Age, Sex)
                 VALUES (@Id, @FirstName, @LastName, @Age, @Gender)
                 """, employee);
    }

    public async Task<int> UpdateEmployeeAsync(EmployeeModel employee, CancellationToken ct)
    {
        using var connection = await _db.CreateConnectionAsync(ct);

        return await connection.ExecuteAsync(
             """
                UPDATE Employees 
                SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Sex = @Gender
                WHERE Id = @Id
                """, new { employee.Id, employee.FirstName, employee.LastName, employee.Age, employee.Gender });
    }

    public async Task<int> DeleteEmployeeAsync(Guid id, CancellationToken ct)
    {
        using var connection = await _db.CreateConnectionAsync(ct);
        return await connection.ExecuteAsync("DELETE FROM Employees WHERE Id = @Id", new { Id = id });
    }

    public async Task<int> DeleteEmployeesAsync(List<Guid> ids, CancellationToken ct)
    {
        using var connection = await _db.CreateConnectionAsync(ct);
        return await connection.ExecuteAsync(
            """
                DELETE FROM Employees
                WHERE Id IN @Ids
                """, new { Ids = ids });
    }

    public async Task<EmployeeModel?> GetEmployeeByIdAsync(Guid id, CancellationToken ct)
    {
        using var connection = await _db.CreateConnectionAsync(ct);

        var employee = await connection.QueryFirstOrDefaultAsync<EmployeeModel>(
            """
            SELECT Id, FirstName, LastName, Age, Sex AS Gender
                    FROM Employees
                    WHERE Id = @Id
            """, new { Id = id });

        return employee;
    }
}

public interface IEmployeeRepository
{
    Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync(CancellationToken ct);

    Task<EmployeeModel?> GetEmployeeByIdAsync(Guid id, CancellationToken ct);

    Task<int> CreateEmployeeAsync(EmployeeModel employee, CancellationToken ct);

    Task<int> UpdateEmployeeAsync(EmployeeModel employee, CancellationToken ct);

    Task<int> DeleteEmployeeAsync(Guid id, CancellationToken ct);
    
    Task<int> DeleteEmployeesAsync(List<Guid> ids, CancellationToken ct);
}