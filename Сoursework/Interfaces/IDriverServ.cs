using System;
using System.Collections.Generic;

namespace Coursework
{
    public interface IDriverServ
    {
        int PersNumber { get; set; }
        string NameDriver { get; set; }
        int Age { get; set; }
        int Experience { get; set; }
        string Category { get; set; }
        string Classic { get; set; }

        List<Tuple<DateTime, DateTime>> BusyPeriods { get; }

        bool IsAvailable(DateTime start, DateTime end);

        void FreePeriod(DateTime start, DateTime end);
    }
}
