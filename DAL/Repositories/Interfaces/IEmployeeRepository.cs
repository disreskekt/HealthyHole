using Domain.Models.Dto;
using HealthyHole.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee AddEmployee(AddEmployeeDto addEmployeeDto);
        Employee EditEmployee(EditEmployeeDto editEmployeeDto);
        void DeleteEmployee(int Id);
        IEnumerable<Employee> GetAllEmployees(Expression<Func<Employee, bool>> predicate = null);
        Employee GetEmployeeById(int Id);
    }
}
