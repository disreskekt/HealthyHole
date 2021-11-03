using HealthyHole.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Dto
{
    public class StatisticsDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public Position Position { get; set; }
        public int RebukeCount { get; set; }
    }
}
