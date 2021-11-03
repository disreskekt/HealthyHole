using DAL.Extensions;
using DAL.Repositories.Interfaces;
using Domain.Extensions;
using Domain.Models.Dto;
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

        public Employee AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            var employee = new Employee
            {
                FirstName = addEmployeeDto.FirstName,
                LastName = addEmployeeDto.LastName,
                Patronymic = addEmployeeDto.Patronymic,
                Position = addEmployeeDto.Position,
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return GetEmployeeById(employee.Id);
        }

        public Employee EditEmployee(EditEmployeeDto editEmployeeDto)
        {
            var employee = _context.Employees.Include(emp => emp.Shifts).FirstOrDefault(emp => emp.Id == editEmployeeDto.Id);

            if (employee is null)
            {
                throw new KeyNotFoundException();
            }

            employee.FirstName = string.IsNullOrWhiteSpace(editEmployeeDto.FirstName) ? employee.FirstName : editEmployeeDto.FirstName;
            employee.LastName = string.IsNullOrWhiteSpace(editEmployeeDto.LastName) ? employee.LastName : editEmployeeDto.LastName;
            employee.Patronymic = string.IsNullOrWhiteSpace(editEmployeeDto.Patronymic) ? employee.Patronymic : editEmployeeDto.Patronymic;
            employee.Position = Enum.IsDefined(typeof(Position), editEmployeeDto.Position) ? editEmployeeDto.Position : employee.Position;
            employee.Shifts = editEmployeeDto.Shifts?.Count() > 0 ? editEmployeeDto.Shifts : employee.Shifts;

            _context.Employees.Update(employee);
            _context.SaveChanges();

            return GetEmployeeById(employee.Id);
        }

        public void DeleteEmployee(int Id)
        {
            var employee = _context.Employees.Find(Id);

            if (employee is null)
            {
                throw new KeyNotFoundException();
            }

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
