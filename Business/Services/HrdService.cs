using Business.Services.Interfaces;
using DAL.Repositories.Interfaces;
using Domain.Extensions;
using Domain.Models.Dto;
using HealthyHole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Services
{
    public class HrdService : IHrdService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public HrdService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public EmployeeDto AddEmployee(AddEmployeeDto addEmployeeDto)
        {
            if (string.IsNullOrWhiteSpace(addEmployeeDto.FirstName) 
                || string.IsNullOrWhiteSpace(addEmployeeDto.LastName)
                || !Enum.IsDefined(typeof(Position), addEmployeeDto.Position))
            {
                throw new ArgumentException();
            }

            var employee = new Employee
            {
                FirstName = addEmployeeDto.FirstName,
                LastName = addEmployeeDto.LastName,
                Patronymic = addEmployeeDto.Patronymic,
                Position = addEmployeeDto.Position,
            };

            _employeeRepository.AddEmployee(employee);

            return employee.ToEmployeeDto();
        }

        public EmployeeDto EditEmployee(EditEmployeeDto editEmployeeDto)
        {
            var employee = _employeeRepository.GetEmployeeById(editEmployeeDto.Id);

            employee.LastName = !string.IsNullOrWhiteSpace(editEmployeeDto.LastName) ? editEmployeeDto.LastName : employee.LastName;
            employee.FirstName = !string.IsNullOrWhiteSpace(editEmployeeDto.FirstName) ? editEmployeeDto.FirstName : employee.FirstName;
            employee.Patronymic = !string.IsNullOrWhiteSpace(editEmployeeDto.Patronymic) ? editEmployeeDto.Patronymic : employee.Patronymic;
            employee.Position = Enum.IsDefined(typeof(Position), editEmployeeDto.Position) ? editEmployeeDto.Position : employee.Position;

            _employeeRepository.EditEmployee(employee);

            return employee.ToEmployeeDto();
        }

        public void DeleteEmployee(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException();
            }

            var employee = GetEmployeeById(Id);

            _employeeRepository.DeleteEmployee(employee);
        }

        public IEnumerable<EmployeeDto> GetAllEmployees(Position? position)
        {
            Expression<Func<Employee, bool>> predicate = null;

            if (position.HasValue)
            {
                if (Enum.IsDefined(typeof(Position), position))
                {
                    predicate = emp => emp.Position.Equals(position);
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            var employees = _employeeRepository.GetAllEmployees(predicate);

            var employeesDto = employees.ToEmployeeDtoList();

            return employeesDto;
        }

        public IEnumerable<Position> GetAllPositions()
        {
            var positions = Enum.GetValues(typeof(Position)).Cast<Position>();

            return positions;
        }

        public EmployeeDto GetEmployeeDtoById(int employeeId)
        {
            var employee = GetEmployeeById(employeeId);

            return employee.ToEmployeeDto();
        }

        public IEnumerable<StatisticsDto> GetStatistics()
        {
            var employees = _employeeRepository.GetAllEmployees();

            var employeesDto = employees.ToEmployeeDtoList();

            return employeesDto.ToStatisticsDtoList();
        }

        private Employee GetEmployeeById(int employeeId)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeId);

            return employee;
        }
    }
}
