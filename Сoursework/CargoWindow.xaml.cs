using System.Windows;

namespace Coursework
{
    public partial class CargoWindow : Window
    {
        public Cargo Cargo { get; private set; }

        public CargoWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Cargo = new Cargo();
            Cargo.Nomination = Nomination.Text;
            Cargo.Quantity = int.TryParse(Quantity.Text, out var q) ? q : 1;
            Cargo.Weight = float.TryParse(Weight.Text, out var w) ? w : 0;
            Cargo.Cost = int.TryParse(Cost.Text, out var c) ? c : 0;
            Cargo.FragileCargo = FragileCargo.IsChecked == true;
            Cargo.CalcInsurance();
            DialogResult = true;
            Close();
        }
    }
}
