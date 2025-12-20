using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Coursework
{
    public class DataStore
    {
        private const string FilePath = "data.json";

        public List<Order> Orders { get; set; }
        public List<Car> Cars { get; set; }
        public List<Driver> Drivers { get; set; }

        public DataStore()
        {
            Orders = new List<Order>();
            Cars = new List<Car>();
            Drivers = new List<Driver>();
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
            Save();
        }

        public void AddCar(Car car)
        {
            Cars.Add(car);
            Save();
        }

        public void AddDriver(Driver driver)
        {
            Drivers.Add(driver);
            Save();
        }

        // 🔥 ВОТ ЕГО НЕ ХВАТАЛО
        public void ClearAll()
        {
            Orders.Clear();
            Cars.Clear();
            Drivers.Clear();
            Save();
        }

        public void Load()
        {
            if (!File.Exists(FilePath))
                return;

            string json = File.ReadAllText(FilePath);
            DataStore data = JsonConvert.DeserializeObject<DataStore>(json);

            if (data == null)
                return;

            Orders = data.Orders ?? new List<Order>();
            Cars = data.Cars ?? new List<Car>();
            Drivers = data.Drivers ?? new List<Driver>();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}
