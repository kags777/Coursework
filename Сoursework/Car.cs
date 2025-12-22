using System;
using System.Collections.Generic;
namespace Coursework
{
    public class Car
    {
        public string Number { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int MaxLoad { get; set; }
        public string Appointment { get; set; }
        public int YearBorn { get; set; }
        public int YearRepairs { get; set; }
        public int Mileage { get; set; }
        public string Photo { get; set; }

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

        public void MileageCalc(int routeLength)
        {
            Mileage += routeLength;
        }
        public void FreePeriod(DateTime start, DateTime end)
        {
            BusyPeriods.RemoveAll(p =>
                p.Item1.Date == start.Date &&  // ← Сравниваем только Date часть
                p.Item2.Date == end.Date);
        }

    }
}
