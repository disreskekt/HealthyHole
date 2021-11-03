using HealthyHole.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void EditEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployees(Expression<Func<Employee, bool>> predicate = null);
        Employee GetEmployeeById(int Id);
    }
}
