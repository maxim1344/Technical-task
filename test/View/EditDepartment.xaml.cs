using System.Windows;
using test.Model;
using test.ViewModel;

namespace test.View
{
    /// <summary>
    /// Логика взаимодействия для EditDepartment.xaml
    /// </summary>
    public partial class EditDepartment : Window
    {
        public EditDepartment(Departments DepartmentToEdit)
        {
            InitializeComponent();
            DataContext = new DataVM();
            DataVM.SelectedDepartment = DepartmentToEdit;
            DataVM.DepartmentName = DepartmentToEdit.NameOfDepartment;            
        }
    }
}
