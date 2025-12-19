using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class CarEditor : UserControl
    {
        private DataStore store;

        public CarEditor(DataStore dataStore)
        {
            InitializeComponent();
            store = dataStore;
            Refresh();
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            var window = new CarWindow();
            if (window.ShowDialog() == true)
            {
                store.AddCar(window.Car);
                Refresh();
            }
        }

        public void Refresh()
        {
            CarList.ItemsSource = null;
            CarList.ItemsSource = store.Cars;
            CarList.DisplayMemberPath = "Number";
        }
    }
}
