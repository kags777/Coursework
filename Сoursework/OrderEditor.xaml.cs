using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coursework

{
    /// <summary>
    /// Логика взаимодействия для OrderEditor.xaml
    /// </summary>


    public partial class OrderEditor : UserControl
    {
        public event Action<Order> OrderSaved;

        public OrderEditor()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Нажата кнопка 'Сохранить'");

            var order = DataContext as Order;

            if (order == null)
            {
                MessageBox.Show("DataContext пустой!");
                return;
            }
            else
            {
                MessageBox.Show("DataContext нормальный, вызываем OrderSaved");
            }

            OrderSaved?.Invoke(order);
            MessageBox.Show("OrderSaved вызван!");
        }
    }

}
