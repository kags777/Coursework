using System;
using System.Collections.Generic;

namespace Coursework
{
    public class Driver
    {
        public int PersNumber { get; set; }
        public string NameDriver { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public string Category { get; set; }
        public string Classic { get; set; }

        public List<Tuple<DateTime, DateTime>> BusyPeriods { get; set; } = new List<Tuple<DateTime, DateTime>>();

        public bool IsAvailable(DateTime start, DateTime end)
        {
            foreach (var period in BusyPeriods)
            {
                if (start < period.Item2 && end > period.Item1)
                    return false;
            }
            return true;
        }

        public void FreePeriod(DateTime start, DateTime end)
        {
            BusyPeriods.RemoveAll(p =>
                p.Item1.Date == start.Date &&  // ← Сравниваем только Date часть
                p.Item2.Date == end.Date);
        }

    }
}