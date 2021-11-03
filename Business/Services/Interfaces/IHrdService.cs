using Domain.Models.Dto;
using HealthyHole.Models;
using System.Collections.Generic;

namespace Business.Services.Interfaces
{
    public interface IHrdService
    {
        EmployeeDto AddEmployee(AddEmployeeDto addEmployeeDto);
        EmployeeDto EditEmployee(EditEmployeeDto editEmployeeDto);
        void DeleteEmployee(int Id);
        IEnumerable<EmployeeDto> GetAllEmployees(Position? position);
        IEnumerable<Position> GetAllPositions();
        EmployeeDto GetEmployeeById(int employeeId);
        IEnumerable<StatisticsDto> GetStatistics();
    }
}
