using System;
using System.Collections.Generic;

namespace Coursework
{
    public interface ICarServ
    {
        string Number { get; set; }
        string Brand { get; set; }
        string Model { get; set; }
        int MaxLoad { get; set; }
        string Appointment { get; set; }
        int YearBorn { get; set; }
        int YearRepairs { get; set; }
        int Mileage { get; }

        string Photo { get; set; }

        List<Tuple<DateTime, DateTime>> BusyPeriods { get; }

        bool IsAvailable(DateTime start, DateTime end);

        void MileageCalc(int routeLength);

        void FreePeriod(DateTime start, DateTime end);
    }
}
