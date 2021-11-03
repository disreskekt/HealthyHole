using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services.Interfaces
{
    public interface IKppService
    {
        void StartShift(int employeeId, string startTime);
        void EndShift(int employeeId, string endTime);
    }
}
