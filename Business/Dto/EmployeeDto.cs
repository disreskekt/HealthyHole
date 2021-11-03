using HealthyHole.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Dto
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Position Position { get; set; }
        public List<ShiftDto> Shifts { get; set; }
    }
}
