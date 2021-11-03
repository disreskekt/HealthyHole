using DAL.Extensions;
using DAL.Repositories.Interfaces;
using HealthyHole.DAL;
using HealthyHole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DAL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly Context _context;
        public EmployeeRepository(Context context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void EditEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployees(Expression<Func<Employee, bool>> predicate = null)
        {
            var employees = _context.Employees.NullSafeWhere(predicate).ToList();

            foreach (var employee in employees)
            {
                _context.Entry(employee).Collection(emp => emp.Shifts).Load();
            }

            return employees;
        }

        public Employee GetEmployeeById(int Id)
        {
            var employee = _context.Employees.FirstOrDefault(emp => emp.Id == Id);

            _context.Entry(employee).Collection(emp => emp.Shifts).Load();

            return employee;
        }
    }
}
