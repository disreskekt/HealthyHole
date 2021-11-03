using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IShiftRepository
    {
        void StartShift(int employeeId, DateTime startTime);
        void EndShift(int employeeId, DateTime endTime);
        bool HasOpenedShift(int employeeId);
    }
}
