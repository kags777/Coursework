using System.Windows;
using System.Windows.Controls;

namespace Coursework
{
    public partial class CarEditor : UserControl
    {
        private DataStore store;
        private MainWindow main;

        public CarEditor(DataStore ds, MainWindow mw)
        {
            InitializeComponent();
            store = ds;
            main = mw;
            RefreshCarList();
        }

        private void RefreshCarList()
        {
            CarListBox.ItemsSource = null;
            CarListBox.ItemsSource = store.Cars;
        }

        private void AddCar_Click(object sender, RoutedEventArgs e)
        {
            CarWindow win = new CarWindow();
            if (win.ShowDialog() == true)
            {
                store.Cars.Add(win.Car);
                store.Save();
                RefreshCarList();
            }
        }

        private void EditCar_Click(object sender, RoutedEventArgs e)
        {
            if (CarListBox.SelectedItem is Car selected)
            {
                CarWindow win = new CarWindow(selected);
                if (win.ShowDialog() == true)
                {
                    store.Save(); // Сохраняем изменения
                    RefreshCarList();
                }
            }
        }

        private void DeleteCar_Click(object sender, RoutedEventArgs e)
        {
            if (CarListBox.SelectedItem is Car selected)
            {
                if (MessageBox.Show($"Удалить машину {selected.Number}?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    store.Cars.Remove(selected);
                    store.Save();
                    RefreshCarList();
                }
            }
        }

        private void ViewCar_Click(object sender, RoutedEventArgs e)
        {
            if (CarListBox.SelectedItem is Car car)
            {
                Window win = new Window
                {
                    Title = $"Машина: {car.Number}",
                    Content = new CarViewer(car),
                    Height = 500,
                    Width = 400
                };
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите машину для просмотра!");
            }
        }
    }
}
