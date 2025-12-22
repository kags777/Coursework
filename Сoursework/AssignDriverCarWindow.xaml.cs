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

        public AssignDriverCarWindow(
            DataStore ds,
            Order order,
            DateTime startDate,
            DateTime endDate)
        {
            InitializeComponent();
            store = ds;

            // 1. Считаем ОБЩИЙ вес всех грузов
            double totalWeight = order.Loads.Sum(c => c.Weight);

            // 2. Фильтруем водителей по занятости
            DriverList.ItemsSource = store.Drivers
                .Where(d => d.IsAvailable(startDate, endDate))
                .ToList();

            // 3. Фильтруем машины:
            // - свободны по датам
            // - выдерживают общий вес
            CarList.ItemsSource = store.Cars
                .Where(car =>
                    car.IsAvailable(startDate, endDate) &&
                    car.MaxLoad >= totalWeight
                )
                .ToList();

            // 4. Если подходящих машин нет — сразу предупреждаем
            if (!CarList.Items.Cast<object>().Any())
            {
                MessageBox.Show(
                    $"Нет машин, подходящих по грузоподъёмности.\n" +
                    $"Общий вес груза: {totalWeight} кг",
                    "Подбор невозможен");
            }
        }

        private void Assign_Click(object sender, RoutedEventArgs e)
        {
            if (DriverList.SelectedItem is Driver driver &&
                CarList.SelectedItem is Car car)
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
