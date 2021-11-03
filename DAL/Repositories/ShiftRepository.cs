using DAL.Repositories.Interfaces;
using HealthyHole.DAL;
using HealthyHole.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly Context _context;
        private readonly IEmployeeRepository _employeeRepository;

        public ShiftRepository(Context context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        public void StartShift(int employeeId, DateTime startTime)
        {
            var shift = new Shift
            {
                Start = startTime,
                EmployeeId = employeeId,
                //Employee = _employeeRepository.GetEmployeeById(employeeId),
                Rebuke = startTime.TimeOfDay > new TimeSpan(9, 0, 0) ? 1 : 0
            };

            _context.Shifts.Add(shift);
            _context.SaveChanges();
        }

        public void EndShift(int employeeId, DateTime endTime)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeId);

            var shift = employee.Shifts.Find(shift => shift.End is null);

            if (shift is null)
            {
                throw new ArgumentException();
            }

            shift.End = endTime;
            shift.Hours = (shift.End.Value - shift.Start).Hours;

            if (employee.Position.Equals(Position.Tester))
            {
                shift.Rebuke = endTime.TimeOfDay < new TimeSpan(21, 0, 0) ? shift.Rebuke + 1 : shift.Rebuke;
            }
            else
            {
                shift.Rebuke = endTime.TimeOfDay < new TimeSpan(18, 0, 0) ? shift.Rebuke + 1 : shift.Rebuke;
            }

            _context.Shifts.Update(shift);
            _context.SaveChanges();
        }

        public bool HasOpenedShift(int employeeId)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeId);

            var openedShift = employee.Shifts?.Find(shift => shift.End is null);

            if (openedShift is null)
            {
                return false;
            }

            return true;
        }
    }
}
