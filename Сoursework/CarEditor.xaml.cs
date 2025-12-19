using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class CarEditor : UserControl
    {
        private DataStore store;

        public CarEditor(DataStore store)
        {
            InitializeComponent();
            this.store = store;
            store.Load();
            CarListBox.ItemsSource = store.Cars;
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            var car = new Car { Number = "Новая машина", Brand = "Марка", Model = "Модель" };
            store.Cars.Add(car);
            CarListBox.Items.Refresh();
            store.Save();
        }

        private void RemoveCar_Click(object sender, RoutedEventArgs e)
        {
            if (CarListBox.SelectedItem is Car car)
            {
                store.Cars.Remove(car);
                CarListBox.Items.Refresh();
                store.Save();
            }
        }
    }
}
