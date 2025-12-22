using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework
{
    internal class Trip : ITripServ
    {
        private string idTrip;
        private string Order;
        private string Car;
        private List<Driver> drivers;
        private DateTime dateLoading;
        private DateTime dateunloading;
        private TimeSpan time;


        public void CalcTime(DateTime dateLoading, DateTime dateunloading)
        {
            time = dateunloading - dateLoading;
        }

    }
}
