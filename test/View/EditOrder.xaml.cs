using System.Text.RegularExpressions;
using System.Windows;
using test.Model;
using test.ViewModel;

namespace test.View
{
    /// <summary>
    /// Логика взаимодействия для EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        public EditOrder(Orders OrderToEdit)
        {
            InitializeComponent();
            DataContext = new DataVM();
            DataVM.SelectedOrder = OrderToEdit;
            DataVM.OrderNumber = OrderToEdit.OrderNumber;
            DataVM.OrderName = OrderToEdit.OrderName;  
            DataVM.OrderEmployee = OrderToEdit.OrderEmployee;
            DataVM.TagsList = OrderToEdit.TagsList;
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
