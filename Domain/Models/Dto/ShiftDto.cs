using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Dto
{
    public class ShiftDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public int Hours { get; set; }
        public int Rebuke { get; set; }
    }
}
