using System.Windows.Controls;

namespace Coursework
{
    public partial class OrderViewer : UserControl
    {
        public OrderViewer(Order order)
        {
            InitializeComponent();
            DataContext = order;
        }
    }
}
