using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coursework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DataStore store = new DataStore(); 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var editor = new OrderEditor();
            editor.DataContext = new Order(); // новый пустой заказ для заполнения
            editor.OrderSaved += OnOrderSaved;

            RightPanel.Content = editor; // показываем справа
            MessageBox.Show("Редактор создан, DataContext = " + (editor.DataContext != null));

        }

        private void OnOrderSaved(Order order)
        {
            Dispatcher.Invoke(() =>
            {
                Console.WriteLine("OnOrderSaved вызван!");
                store.Orders.Add(order);
                store.Save();
                Console.WriteLine("Файл должен сохраниться");

                MessageBox.Show("Заказ сохранён!");
                RightPanel.Content = null;
                MessageBox.Show("OnOrderSaved вызван, Orders.Count = " + store.Orders.Count);
            });

        }
    }
}
