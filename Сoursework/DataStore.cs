using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace Coursework
{
    public class DataStore
    {
        private const string FilePath = "data.json";

        public List<Order> Orders { get; set; } = new List<Order>();

        // Загрузка данных
        public void Load()
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                var data = JsonConvert.DeserializeObject<DataStore>(json);
                if (data != null)
                {
                    Orders = data.Orders;
                }
            }
        }

        // Сохранение данных
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(FilePath, json);
            MessageBox.Show($"JSON сохранён в {FilePath}");
        }
    }
}

