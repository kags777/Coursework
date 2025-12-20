using System;
using System.Collections.Generic;

namespace Coursework
{
    public class Order
    {
        public DateTime DateLoading { get; set; }
        public DateTime DateUnloading { get; set; }

        public Client Client { get; set; }

        public string LoadingAddress { get; set; }
        public string UnloadingAddress { get; set; }

        public float RouteLength { get; set; }

        // 🔥 ВОТ ОНО
        public List<Cargo> Loads { get; set; }

        public Car AssignedCar { get; set; }
        public Driver AssignedDriver { get; set; }

        // Created / Active / Completed
        public string Status { get; set; }

        public float Cost { get; set; }

        public Order()
        {
            Loads = new List<Cargo>();
            Status = "Created";
        }

        public override string ToString()
        {
            return Status + " | " +
                   DateLoading.ToShortDateString() + " → " +
                   DateUnloading.ToShortDateString();
        }
    }
}
