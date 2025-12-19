using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Coursework
{
    public class DataStore
    {
        private const string FilePath = "data.json";

        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Car> Cars { get; set; } = new List<Car>();
        public List<Driver> Drivers { get; set; } = new List<Driver>();

        public void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var data = JsonConvert.DeserializeObject<DataStore>(json);
                if (data != null)
                {
                    Orders = data.Orders ?? new List<Order>();
                    Cars = data.Cars ?? new List<Car>();
                    Drivers = data.Drivers ?? new List<Driver>();
                }
            }
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public void AddOrder(Order o) => Orders.Add(o);
        public void AddCar(Car c) => Cars.Add(c);
        public void AddDriver(Driver d) => Drivers.Add(d);
    }
}
