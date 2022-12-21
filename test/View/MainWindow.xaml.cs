using System.Windows;
using System.Windows.Controls;

namespace test.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ListView AllDepartmentsView;
        public static ListView AllEmployeesView;
        public static ListView AllOrdersView;
        public static ListView AllTagsView;

        public MainWindow()
        {
            InitializeComponent();            
            AllDepartmentsView = ViewAllDepartments;
            AllEmployeesView = ViewAllEmployees;
            AllOrdersView = ViewAllOrders;
            AllTagsView = ViewAllTags;
        }
    }
}
