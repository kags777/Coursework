using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Coursework
{
    public partial class CarViewer : UserControl
    {
        public CarViewer(Car car)
        {
            InitializeComponent();

            NumberText.Text = $"Номер: {car.Number}";
            BrandText.Text = $"Марка: {car.Brand}";
            ModelText.Text = $"Модель: {car.Model}";
            MaxLoadText.Text = $"Грузоподъемность: {car.MaxLoad}";
            AppointmentText.Text = $"Назначение: {car.Appointment}";
            YearBornText.Text = $"Год выпуска: {car.YearBorn}";
            YearRepairsText.Text = $"Год ремонта: {car.YearRepairs}";
            MileageText.Text = $"Пробег: {car.Mileage} км";

            if (!string.IsNullOrEmpty(car.Photo) && File.Exists(car.Photo))
            {
                try
                {
                    CarImage.Source = new BitmapImage(new Uri(car.Photo, UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    CarImage.Source = null;
                }
            }
        }
    }
}
