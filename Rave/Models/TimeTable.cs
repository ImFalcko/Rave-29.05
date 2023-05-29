using System;
using System.Collections.Generic;
using System.Text;

namespace Rave.Models
{
    public class TimeTable
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
    }
}
