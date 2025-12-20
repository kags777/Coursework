using System;
using System.Windows;
using Microsoft.Win32;

namespace Coursework
{
    public partial class CarWindow : Window
    {
        public Car Car { get; private set; }

        public CarWindow(Car car = null)
        {
            InitializeComponent();

            if (car != null)
            {
                Car = car;
                Number.Text = car.Number;
                Brand.Text = car.Brand;
                Model.Text = car.Model;
                MaxLoad.Text = car.MaxLoad.ToString();
                Appointment.Text = car.Appointment;
                YearBorn.Text = car.YearBorn.ToString();
                YearRepairs.Text = car.YearRepairs.ToString();
                Mileage.Text = car.Mileage.ToString();
                Photo.Text = car.Photo;
            }
        }

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (dlg.ShowDialog() == true)
            {
                Photo.Text = dlg.FileName;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Car == null) Car = new Car();

            Car.Number = Number.Text;
            Car.Brand = Brand.Text;
            Car.Model = Model.Text;
            Car.MaxLoad = int.TryParse(MaxLoad.Text, out var m) ? m : 0;
            Car.Appointment = Appointment.Text;
            Car.YearBorn = int.TryParse(YearBorn.Text, out var yb) ? yb : 0;
            Car.YearRepairs = int.TryParse(YearRepairs.Text, out var yr) ? yr : 0;
            Car.Mileage = int.TryParse(Mileage.Text, out var km) ? km : 0;
            Car.Photo = Photo.Text;

            DialogResult = true;
            Close();
        }
    }
}
