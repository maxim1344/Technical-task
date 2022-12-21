using System.Windows;
using test.Model;
using test.ViewModel;

namespace test.View
{
    /// <summary>
    /// Логика взаимодействия для EditEmployee.xaml
    /// </summary>
    public partial class EditEmployee : Window
    {
        public EditEmployee(Employees employeeToEdit)
        {
            InitializeComponent();
            DataContext = new DataVM();
            DataVM.SelectedEmployee = employeeToEdit;
            DataVM.Name = employeeToEdit.Name;
            DataVM.Surname = employeeToEdit.Surname;
            DataVM.Patronymic = employeeToEdit.Patronymic;
            DataVM.DateOfBirth = employeeToEdit.DateOfBirth;
            DataVM.Gender = (DataVM.gender)employeeToEdit.Gender;
        }
    }
}
