using System;
using System.Linq;
using System.Windows;

namespace Coursework
{
    public partial class AssignDriverCarWindow : Window
    {
        public Driver SelectedDriver { get; private set; }
        public Car SelectedCar { get; private set; }

        private DataStore store;

        // Новый конструктор с проверкой дат
        public AssignDriverCarWindow(DataStore ds, DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            store = ds;

            // Фильтруем водителей и машины по занятости
            DriverList.ItemsSource = store.Drivers
                .Where(d => d.IsAvailable(startDate, endDate))
                .ToList();

            CarList.ItemsSource = store.Cars
                .Where(c => c.IsAvailable(startDate, endDate))
                .ToList();
        }

        public AssignDriverCarWindow(DataStore ds) : this(ds, DateTime.MinValue, DateTime.MinValue)
        {
            // оставляем для совместимости, если даты не нужны
        }

        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            if (DriverList.SelectedItem is Driver driver && CarList.SelectedItem is Car car)
            {
                SelectedDriver = driver;
                SelectedCar = car;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Выберите водителя и машину!");
            }
        }
    }
}
