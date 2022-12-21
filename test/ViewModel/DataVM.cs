using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using test.Model;
using test.View;

namespace test.ViewModel
{
    public class DataVM : INotifyPropertyChanged
    {
        public static string DepartmentName { get; set; }
        public static Employees Head { get; set; }

        public static string Surname { get; set; }
        public static string Name { get; set; }
        public static string Patronymic { get; set; }
        public static DateTime? DateOfBirth { get; set; }
        public static gender Gender { get; set; }
        public enum gender
        {
            Мужской,
            Женский
        }
        public static gender[] GenderList
        {
            get { return (gender[])Enum.GetValues(typeof(gender)); }
        }
        public static Departments Department { get; set; }

        public static int OrderNumber { get; set; }
        public static string OrderName { get; set; }
        public static Employees OrderEmployee { get; set; }
        public static ICollection<Tags> Tags { get; set; }
        public static List<string> TagsList { get; set; }

        public static string TagName { get; set; }

        public static Employees SelectedEmployee { get; set; }
        public static Departments SelectedDepartment { get; set; }
        public static Orders SelectedOrder { get; set; }
        public static Tags SelectedTag { get; set; }

        public TabItem SelectedTabItem { get; set; }

        private List<Tags> allTags = DataWorker.GetAllTags();
        public List<Tags> AllTags
        {
            get { return allTags; }
            set
            {
                allTags = value;
                NotifyPropertyChanged("AllTags");
            }
        }

        private List<Departments> allDepartments = DataWorker.GetAllDepartments();
        public List<Departments> AllDepartments
        {
            get { return allDepartments; }
            set
            {
                allDepartments = value;
                NotifyPropertyChanged("AllDepartments");
            }
        }

        private List<Orders> allOrders = DataWorker.GetAllOrders();
        public List<Orders> AllOrders
        {
            get
            {
                return allOrders;
            }
            private set
            {
                allOrders = value;
                NotifyPropertyChanged("AllOrders");
            }
        }

        private List<Employees> allEmployees = DataWorker.GetAllEmployees();
        public List<Employees> AllEmployees
        {
            get
            {
                return allEmployees;
            }
            private set
            {
                allEmployees = value;
                NotifyPropertyChanged("AllEmployees");
            }
        }

        #region RelayCommands
        private RelayCommand openAddDepartmentsWin;
        public RelayCommand OpenAddDepartmentsWin
        {
            get
            {
                return openAddDepartmentsWin ?? new RelayCommand(obj =>
                {
                    OpenAddDepartmentsWindow();
                }
                );
            }
        }

        private RelayCommand openAddEmployeesWin;
        public RelayCommand OpenAddEmployeesWin
        {
            get
            {
                return openAddEmployeesWin ?? new RelayCommand(obj =>
                {
                    OpenAddEmployeesWindow();
                }
                );
            }
        }

        private RelayCommand openAddOrdersWin;
        public RelayCommand OpenAddOrdersWin
        {
            get
            {
                return openAddOrdersWin ?? new RelayCommand(obj =>
                {
                    OpenAddOrdersWindow();
                }
                );
            }
        }

        private RelayCommand addNewDepartment;
        public RelayCommand AddNewDepartment
        {
            get
            {
                return addNewDepartment ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Укажите название подразделения");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateDepartment(DepartmentName, Head);
                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewEmployee;
        public RelayCommand AddNewEmployee
        {
            get
            {
                return addNewEmployee ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    if (Surname == null || Surname.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Укажите фамилию сотрудника");
                    }
                    else if (Name == null || Name.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Укажите имя сотрудника");
                    }
                    else if (Patronymic == null || Patronymic.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Укажите отчество сотрудника");
                    }
                    else if (DateOfBirth == null)
                    {
                        ShowMessageToUser("Укажите дату рождения сотрудника");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateEmployee(Name, Surname, Patronymic, (DateTime)DateOfBirth, Gender, Department);
                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand addNewOrder;
        public RelayCommand AddNewOrder
        {
            get
            {
                return addNewOrder ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    List<Tags> TagListForOrder = new List<Tags>();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AddNewOrder))
                        {
                            foreach (Tags item in (window as AddNewOrder).SelectTags.Items)
                            {
                                TagListForOrder.Add(item);
                            }
                        }
                    }
                    string resultStr = "";
                    if (OrderNumber == 0)
                    {
                        ShowMessageToUser("Укажите номер заказа");
                    }
                    else if (OrderName == null || OrderName.Replace(" ", "").Length == 0)
                    {
                        ShowMessageToUser("Укажите название товара");
                    }
                    else if (OrderEmployee == null)
                    {
                        ShowMessageToUser("Укажите сотрудника");
                    }
                    else
                    {
                        resultStr = DataWorker.CreateOrder(OrderNumber, OrderName, OrderEmployee, TagListForOrder);
                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }
                );
            }
        }

        private RelayCommand openAddTagWin;
        public RelayCommand OpenAddTagWin
        {
            get
            {
                return openAddTagWin ?? new RelayCommand(obj =>
                {
                    OpenAddTagWindow();
                }
                );
            }
        }

        private RelayCommand deleteItemTag;
        public RelayCommand DeleteItemTag
        {
            get
            {
                return deleteItemTag ?? new RelayCommand(obj =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AddNewOrder))
                        {
                            if ((window as AddNewOrder).SelectTags.SelectedItem == null)
                            {
                                ShowMessageToUser("Выберите элемент для удаления");
                            }
                            else
                            {
                                (window as AddNewOrder).SelectTags.Items.Remove((window as AddNewOrder).SelectTags.SelectedItem);
                            }
                        }
                    }
                }
                );
            }
        }

        private RelayCommand addItemTag;
        public RelayCommand AddItemTag
        {
            get
            {
                return addItemTag ?? new RelayCommand(obj =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AddNewOrder))
                        {
                            if ((window as AddNewOrder).ViewAllTags.SelectedItem == null)
                            {
                                ShowMessageToUser("Выберите элемент в общем списке тегов");
                            }
                            else
                            {
                                (window as AddNewOrder).SelectTags.Items.Add((window as AddNewOrder).ViewAllTags.SelectedItem);
                            }
                        }
                    }
                }
                );
            }
        }

        private RelayCommand deleteEditItemTag;
        public RelayCommand DeleteEditItemTag
        {
            get
            {
                return deleteEditItemTag ?? new RelayCommand(obj =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(EditOrder))
                        {
                            if ((window as EditOrder).SelectTags.SelectedItem == null)
                            {
                                ShowMessageToUser("Выберите элемент для удаления");
                            }
                            else
                            {
                                List<string> temp = new List<string>();
                                foreach (var item in (window as EditOrder).SelectTags.Items)
                                {
                                    temp.Add(item.ToString());
                                }
                                temp.Remove((window as EditOrder).SelectTags.SelectedItem.ToString());

                                (window as EditOrder).SelectTags.ItemsSource = temp;
                            }
                        }
                    }
                }
                );
            }
        }

        private RelayCommand addEditItemTag;
        public RelayCommand AddEditItemTag
        {
            get
            {
                return addEditItemTag ?? new RelayCommand(obj =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(EditOrder))
                        {
                            if ((window as EditOrder).ViewAllTags.SelectedItem == null)
                            {
                                ShowMessageToUser("Выберите элемент в общем списке тегов");
                            }
                            else
                            {
                                List<string> temp = new List<string>();
                                foreach (var item in (window as EditOrder).SelectTags.Items)
                                {
                                    temp.Add(item.ToString());
                                }
                                Tags addItem = (Tags)(window as EditOrder).ViewAllTags.SelectedItem;
                                temp.Add(addItem.Name);
                                (window as EditOrder).SelectTags.ItemsSource = temp;
                                (window as EditOrder).ViewAllTags.SelectedItem = null;
                                (window as EditOrder).SelectTags.SelectedItem = null;
                            }
                        }
                    }
                }
                );
            }
        }

        private RelayCommand addLbItem;
        public RelayCommand AddLbItem
        {
            get
            {
                return addLbItem ?? new RelayCommand(obj =>
                {
                    ListBox lb = obj as ListBox;
                    if (TagName == null)
                    {
                        ShowMessageToUser("Укажите имя добавляемого тега");
                    }
                    else
                    {
                        lb.Items.Add(TagName);
                        var s = lb.SelectedItems;
                        string resultStr = "";
                        TagName = null;
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(AddNewTags))
                            {
                                (window as AddNewTags).TagName.Text = "";
                            }
                        }
                    }

                }
                );
            }
        }

        private RelayCommand deleteLbItem;
        public RelayCommand DeleteLbItem
        {
            get
            {
                return deleteLbItem ?? new RelayCommand(obj =>
                {
                    ListBox lb = obj as ListBox;
                    if (lb.SelectedItem == null)
                    {
                        ShowMessageToUser("Выберете элемент из списка");
                    }
                    else
                    {
                        lb.Items.Remove(lb.SelectedItem);
                        string resultStr = "";
                    }
                }
                );
            }
        }

        private RelayCommand addNewTag;
        public RelayCommand AddNewTag
        {
            get
            {
                return addNewTag ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    List<string> tempTagNames = new List<string>();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AddNewTags))
                        {
                            var s = (window as AddNewTags).LB.Items;
                            foreach (var item in s)
                            {
                                tempTagNames.Add(item.ToString());
                            }
                        }
                    }
                    ShowMessageToUser(DataWorker.CreateTags(tempTagNames));
                    UpdateAllDataView();
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AddNewOrder))
                        {
                            (window as AddNewOrder).ViewAllTags.ItemsSource = null;
                            (window as AddNewOrder).ViewAllTags.Items.Clear();
                            (window as AddNewOrder).ViewAllTags.ItemsSource = AllTags;
                            (window as AddNewOrder).ViewAllTags.Items.Refresh();
                        }
                    }
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(EditOrder))
                        {
                            (window as EditOrder).ViewAllTags.ItemsSource = null;
                            (window as EditOrder).ViewAllTags.Items.Clear();
                            (window as EditOrder).ViewAllTags.ItemsSource = AllTags;
                            (window as EditOrder).ViewAllTags.Items.Refresh();
                        }
                    }
                    wnd.Close();
                }
                );
            }
        }

        private RelayCommand deleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return deleteItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Выберите элемент для удаления";

                    if (SelectedTabItem.Name == "EmployeesTab" && SelectedEmployee != null)
                    {
                        resultStr = DataWorker.DeleteEmployye(SelectedEmployee);
                        UpdateAllDataView();
                    }
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                        UpdateAllDataView();
                    }
                    if (SelectedTabItem.Name == "OrdersTab" && SelectedOrder != null)
                    {
                        resultStr = DataWorker.DeleteOrder(SelectedOrder);
                        UpdateAllDataView();
                    }
                    if (SelectedTabItem.Name == "TagsTab" && SelectedTag != null)
                    {
                        resultStr = DataWorker.DeleteTag(SelectedTag);
                        UpdateAllDataView();
                    }
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }
                );
            }
        }

        private RelayCommand editItem;
        public RelayCommand EditItem
        {
            get
            {
                return editItem ?? new RelayCommand(obj =>
                {
                    string resultStr = "Выберите элемент для редактирования";

                    if (SelectedEmployee == null && SelectedDepartment == null && SelectedOrder == null)
                    {
                        ShowMessageToUser(resultStr);
                    }
                    else if (SelectedTabItem.Name == "EmployeesTab" && SelectedEmployee != null)
                    {
                        OpenEditEmployeesWindow(SelectedEmployee);
                    }
                    if (SelectedTabItem.Name == "DepartmentsTab" && SelectedDepartment != null)
                    {
                        OpenEditDepartmentsWindow(SelectedDepartment);
                    }
                    if (SelectedTabItem.Name == "OrdersTab" && SelectedOrder != null)
                    {
                        OpenEditOrdersWindow(SelectedOrder);
                    }
                }
                );
            }
        }

        private RelayCommand editEmployee;
        public RelayCommand EditEmployee
        {
            get
            {
                return editEmployee ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран сотрудник";
                    if (SelectedEmployee != null)
                    {
                        resultStr = DataWorker.EditEmployee(SelectedEmployee, Name, Surname, Patronymic, (DateTime)DateOfBirth, Gender, Department);

                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
        }

        private RelayCommand editDepartment;
        public RelayCommand EditDepartment
        {
            get
            {
                return editDepartment ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрано подразделение";
                    if (SelectedDepartment != null)
                    {
                        resultStr = DataWorker.EditDepartment(SelectedDepartment, DepartmentName, Head);
                        ShowMessageToUser(resultStr);
                        UpdateAllDataView();
                        SetNullValuesToProperties();
                        window.Close();
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
        }

        private RelayCommand editOrder;
        public RelayCommand EditOrder
        {
            get
            {
                return editOrder ?? new RelayCommand(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран заказ";
                    if (SelectedOrder != null)
                    {
                        List<string> temp = new List<string>();
                        foreach (Window wnd in Application.Current.Windows)
                        {
                            if (wnd.GetType() == typeof(EditOrder))
                            {
                                foreach (var item in (window as EditOrder).SelectTags.Items)
                                {
                                    temp.Add(item.ToString());
                                }
                            }
                        }
                        if (OrderNumber == 0)
                        {
                            ShowMessageToUser("Укажите номер заказа");
                        }
                        else if (OrderName == null || OrderName.Replace(" ", "").Length == 0)
                        {
                            ShowMessageToUser("Укажите название товара");
                        }
                        else if (OrderEmployee == null)
                        {
                            ShowMessageToUser("Укажите сотрудника");
                        }
                        else
                        {
                            resultStr = DataWorker.EditOrder(SelectedOrder, OrderNumber, OrderName, OrderEmployee, temp);
                            ShowMessageToUser(resultStr);
                            UpdateAllDataView();
                            SetNullValuesToProperties();
                            window.Close();
                        }
                    }
                    else ShowMessageToUser(resultStr);
                }
                );
            }
        }
        #endregion


        #region OpenWindows
        private void OpenAddTagWindow()
        {
            AddNewTags addNewTags = new AddNewTags();
            SetCenterPositionAndOpen(addNewTags);
        }

        private void OpenAddDepartmentsWindow()
        {
            AddNewDepartment addNewDepartment = new AddNewDepartment();
            SetCenterPositionAndOpen(addNewDepartment);
        }

        private void OpenAddEmployeesWindow()
        {
            AddNewEmployee addNewEmployee = new AddNewEmployee();
            SetCenterPositionAndOpen(addNewEmployee);
        }

        private void OpenAddOrdersWindow()
        {
            AddNewOrder addNewOrder = new AddNewOrder();
            SetCenterPositionAndOpen(addNewOrder);
        }

        private void OpenEditDepartmentsWindow(Departments dep)
        {
            EditDepartment addEditDepartment = new EditDepartment(dep);
            SetCenterPositionAndOpen(addEditDepartment);
        }

        private void OpenEditEmployeesWindow(Employees emp)
        {
            EditEmployee addEditEmployee = new EditEmployee(emp);
            SetCenterPositionAndOpen(addEditEmployee);
        }

        private void OpenEditOrdersWindow(Orders ord)
        {
            EditOrder addEditOrder = new EditOrder(ord);
            SetCenterPositionAndOpen(addEditOrder);
        }

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        } 
        #endregion

        private void SetNullValuesToProperties()
        {
            DepartmentName = null;
            Head = null;

            Name = null;
            Surname = null;
            Patronymic = null;
            DateOfBirth= null;           
            Department = null;

            OrderNumber = 0;
            OrderName = null;
            OrderEmployee = null;

            TagName = null;
        }

        private void UpdateAllDataView()
        {
            UpdateAllDepartmentsView();
            UpdateAllEmployeesView();
            UpdateAllOrdersView();
            UpdateAllTagsView();
        }

        private void UpdateAllDepartmentsView()
        {
            allDepartments = DataWorker.GetAllDepartments();
            MainWindow.AllDepartmentsView.ItemsSource = null;
            MainWindow.AllDepartmentsView.Items.Clear();
            MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
            MainWindow.AllDepartmentsView.Items.Refresh();
        }

        private void UpdateAllEmployeesView()
        {
            allEmployees = DataWorker.GetAllEmployees();
            MainWindow.AllEmployeesView.ItemsSource = null;
            MainWindow.AllEmployeesView.Items.Clear();
            MainWindow.AllEmployeesView.ItemsSource = AllEmployees;
            MainWindow.AllEmployeesView.Items.Refresh();
        }

        private void UpdateAllOrdersView()
        {
            allOrders = DataWorker.GetAllOrders();
            MainWindow.AllOrdersView.ItemsSource = null;
            MainWindow.AllOrdersView.Items.Clear();
            MainWindow.AllOrdersView.ItemsSource = AllOrders;
            MainWindow.AllOrdersView.Items.Refresh();
        }

        private void UpdateAllTagsView()
        {
            allTags = DataWorker.GetAllTags();
            MainWindow.AllTagsView.ItemsSource = null;
            MainWindow.AllTagsView.Items.Clear();
            MainWindow.AllTagsView.ItemsSource = allTags;
            MainWindow.AllTagsView.Items.Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
