using Domain.Models.Dto;
using HealthyHole.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Extensions
{
    public static class ModelCastExtensions
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Patronymic = employee.Patronymic,
                Position = employee.Position,
                Shifts = employee.Shifts.ToShiftDtoList()
            };
        }

        public static List<EmployeeDto> ToEmployeeDtoList(this IEnumerable<Employee> employeeList)
        {
            var dtoList = new List<EmployeeDto>();

            foreach (var employee in employeeList)
            {
                dtoList.Add(employee.ToEmployeeDto());
            }

            return dtoList;
        }

        public static ShiftDto ToShiftDto(this Shift shift)
        {
            return new ShiftDto
            {
                Id = shift.Id,
                Start = shift.Start,
                End = shift.End,
                Hours = shift.Hours,
                Rebuke = shift.Rebuke
            };
        }

        public static List<ShiftDto> ToShiftDtoList(this IEnumerable<Shift> shiftList)
        {
            var dtoList = new List<ShiftDto>();

            foreach (var shift in shiftList)
            {
                dtoList.Add(shift.ToShiftDto());
            }

            return dtoList;
        }

        public static StatisticsDto ToStatisticsDto(this EmployeeDto employeeDto)
        {
            var rebukeCount = 0;

            employeeDto.Shifts.ForEach(shift => rebukeCount = shift.Rebuke);

            return new StatisticsDto
            {
                LastName = employeeDto.LastName,
                FirstName = employeeDto.FirstName,
                Patronymic = employeeDto.Patronymic,
                Position = employeeDto.Position,
                RebukeCount = rebukeCount
            };
        }

        public static List<StatisticsDto> ToStatisticsDtoList(this IEnumerable<EmployeeDto> employeesDto)
        {
            var statisticsDtoList = new List<StatisticsDto>();

            foreach (var employeeDto in employeesDto)
            {
                statisticsDtoList.Add(employeeDto.ToStatisticsDto());
            }

            return statisticsDtoList;
        }
    }
}
