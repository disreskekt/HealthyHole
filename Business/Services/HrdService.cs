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
            if (string.IsNullOrWhiteSpace(addEmployeeDto.FirstName) || string.IsNullOrWhiteSpace(addEmployeeDto.LastName) || !Enum.IsDefined(typeof(Position), addEmployeeDto.Position))
            {
                throw new ArgumentException();
            }

            var employee = _employeeRepository.AddEmployee(addEmployeeDto);

            return employee.ToEmployeeDto();
        }

        public EmployeeDto EditEmployee(EditEmployeeDto editEmployeeDto)
        {
            if (editEmployeeDto.Id <= 0)
            {
                throw new ArgumentException();
            }

            var employee = _employeeRepository.EditEmployee(editEmployeeDto);

            return employee.ToEmployeeDto();
        }

        public void DeleteEmployee(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException();
            }

            _employeeRepository.DeleteEmployee(Id);
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
            return Enum.GetValues(typeof(Position)).Cast<Position>();
        }

        public EmployeeDto GetEmployeeById(int employeeId)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeId);

            return employee.ToEmployeeDto();
        }

        public IEnumerable<StatisticsDto> GetStatistics()
        {
            var employees = _employeeRepository.GetAllEmployees();

            var employeesDto = employees.ToEmployeeDtoList();

            return employeesDto.ToStatisticsDtoList();
        }
    }
}
