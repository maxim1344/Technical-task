using System.Text.RegularExpressions;
using System.Windows;

namespace test.View
{
    /// <summary>
    /// Логика взаимодействия для AddNewOrder.xaml
    /// </summary>
    public partial class AddNewOrder : Window
    {
        public AddNewOrder()
        {
            InitializeComponent();
            
        }

        private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }

}
