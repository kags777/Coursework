using System.Linq;
using System.Windows;

namespace Coursework
{
    public partial class AssignDriverCarWindow : Window
    {
        public Driver SelectedDriver { get; private set; }
        public Car SelectedCar { get; private set; }

        private DataStore store;

        public AssignDriverCarWindow(DataStore ds)
        {
            InitializeComponent();
            store = ds;

            // показываем всех водителей и машины
            DriverList.ItemsSource = store.Drivers;
            CarList.ItemsSource = store.Cars;
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
