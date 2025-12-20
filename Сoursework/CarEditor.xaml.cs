using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class CarEditor : UserControl
    {
        private DataStore store;

        public CarEditor(DataStore ds)
        {
            InitializeComponent();
            store = ds;
            Refresh();
        }

        private void Refresh()
        {
            CarList.ItemsSource = null;
            CarList.ItemsSource = store.Cars;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var w = new CarWindow();
            if (w.ShowDialog() == true)
            {
                store.Cars.Add(w.Car);
                store.Save();
                Refresh();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (CarList.SelectedItem is Car car)
            {
                var w = new CarWindow(car);
                if (w.ShowDialog() == true)
                {
                    store.Save();
                    Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (CarList.SelectedItem is Car car)
            {
                store.Cars.Remove(car);
                store.Save();
                Refresh();
            }
        }
    }
}
