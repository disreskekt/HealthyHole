using Business.Services.Interfaces;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class KppService : IKppService
    {
        private readonly IShiftRepository _shiftRepository;
        public KppService(IShiftRepository shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public void StartShift(int employeeId, string startTime)
        {
            if (!DateTime.TryParse(startTime, out DateTime startDateTime))
            {
                throw new FormatException();
            }

            if (_shiftRepository.HasOpenedShift(employeeId))
            {
                throw new AccessViolationException();
            }

            if (!startDateTime.Date.Equals(DateTime.Today))
            {
                throw new FormatException();
            }

            _shiftRepository.StartShift(employeeId, startDateTime);
        }

        public void EndShift(int employeeId, string endTime)
        {
            if (!DateTime.TryParse(endTime, out DateTime endDateTime))
            {
                throw new FormatException();
            }

            if (!_shiftRepository.HasOpenedShift(employeeId))
            {
                throw new AccessViolationException();
            }

            if (!endDateTime.Date.Equals(DateTime.Today))
            {
                throw new FormatException();
            }

            _shiftRepository.EndShift(employeeId, endDateTime);
        }
    }
}
