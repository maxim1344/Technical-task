using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using test.Model.Data;

namespace test.Model
{
    public static class DataWorker
    {
        //получить все теги
        public static List<Tags> GetAllTags()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Tags.ToList();
                return result;
            }
        }

        //получить все отделы
        public static List<Departments> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }            
        }

        //получить всех сотрудников
        public static List<Employees> GetAllEmployees()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Employees.ToList();
                return result;
            }
        }

        //получить все заказы
        public static List<Orders> GetAllOrders()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Orders.ToList();
                return result;
            }
        }

        // создать теги
        public static string CreateTags(List<string> names)
        {
            string result = "Тег с таким именем уже существует в базе данных";
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var item in names)
                {
                    bool checkIsExist = db.Tags.Any(el => el.Name == item);
                    if (!checkIsExist)
                    {
                        Tags newTags = new Tags
                        {
                            Name = item
                        };
                        db.Tags.Add(newTags);
                        db.SaveChanges();
                        result = "Готово";
                    }
                    
                }
                return result;

            }
        }

        // создать подразделения
        public static string CreateDepartment(string name, Employees employee)
        {
            string result = "Данная позиция уже присутствует в базе данных";
            using(ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Departments.Any(el => el.NameOfDepartment == name);
                if (!checkIsExist) 
                {
                    if (employee == null)
                    {
                        Departments newDeparment = new Departments
                        {
                            NameOfDepartment = name,
                        };
                        db.Departments.Add(newDeparment);
                    }
                    else
                    {
                        Employees employeeDep = db.Employees.FirstOrDefault(d => d.Id == employee.Id);
                        Departments newDeparmentWithEmp = new Departments
                        {
                            NameOfDepartment = name,
                            EmployeesId = employee.Id
                        };
                        db.Departments.Add(newDeparmentWithEmp);
                        db.SaveChanges();
                        Departments dep = db.Departments.FirstOrDefault(d => d.Id == newDeparmentWithEmp.Id);
                        employeeDep.DepartmentId = dep.Id;
                    }                   
                    db.SaveChanges();
                    result = "Готово";
                }
                return result;
            }
        }

        // создать сотрудника
        public static string CreateEmployee(string name, string surname, string patronymic, DateTime dateOfBirth, Enum gender, Departments department)
        {
            string result = "Данная позиция уже присутствует в базе данных";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Employees.Any(el => el.Name == name
                && el.Surname == surname
                && el.Patronymic == patronymic
                && el.DateOfBirth == dateOfBirth
                && el.Gender.Equals(gender));

                if (!checkIsExist)
                {
                    if (department == null)
                    {
                        Employees newEmployee = new Employees
                        {
                            Name = name,
                            Surname = surname,
                            Patronymic = patronymic,
                            DateOfBirth = dateOfBirth,
                            Gender = (Employees.gender)gender
                        };
                        db.Employees.Add(newEmployee);
                    }
                    else
                    {
                        Employees newEmployeeWithDep = new Employees
                        {
                            Name = name,
                            Surname = surname,
                            Patronymic = patronymic,
                            DateOfBirth = dateOfBirth,
                            Gender = (Employees.gender)gender,
                            DepartmentId = department.Id
                        };
                        db.Employees.Add(newEmployeeWithDep);
                    }                   
                    db.SaveChanges();
                    result = "Готово";
                }
                return result;
            }
        }

        // создать заказ
        public static string CreateOrder(int orderNumber, string orderName, Employees orderEmployee, List<Tags> tags)
        {
            string result = "Данная позиция уже присутствует в базе данных";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Orders.Any(el => el.OrderNumber == orderNumber
                && el.OrderName == orderName);
                if (!checkIsExist)
                {
                    Orders newOrder = new Orders();
                    newOrder.OrderNumber = orderNumber;
                    newOrder.OrderName = orderName;
                    newOrder.EmployeeId = orderEmployee.Id;
                    newOrder.Tags = new List<Tags>();                  
                    for (int i = 0; i < tags.Count; i++)
                    {
                        Tags tg = db.Tags.FirstOrDefault(t => t.Name == tags[i].Name);
                        newOrder.Tags.Add(tg);
                    }
                    db.Orders.Add(newOrder);                   
                    db.SaveChanges();
                    result = "Готово";
                }
                return result;
            }
        }

        //удалить подразделение
        public static string DeleteDepartment(Departments department)
        {
            string result = "NotExist";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = "Готово! Подразделение " + department.NameOfDepartment + " было удалено";
            }
            return result;
        }

        //уволить сотрудника
        public static string DeleteEmployye(Employees employee)
        {
            string result = "NotExist";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
                result = "Готово! Сотрудник " + employee.Name +", "+employee.Surname+ " был уволен";
            }
            return result;
        }

        //удалить заказ
        public static string DeleteOrder(Orders order)
        {
            string result = "NotExist";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Orders.Remove(order);
                db.SaveChanges();
                result = "Готово! Заказ " + order.OrderName + " был удален";
            }
            return result;
        }

        //удалить тег
        public static string DeleteTag(Tags tags)
        {
            string result = "NotExist";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Tags.Remove(tags);
                db.SaveChanges();
                result = "Готово! Тег " + tags.Name + " был удален";
            }
            return result;
        }

        //изменить подразделение
        public static string EditDepartment(Departments oldDepartment, string newName, Employees newEmployee)
        {
            string result = "NotExist";
            using (ApplicationContext db = new ApplicationContext())
            {
                
                Departments department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                Employees employee = db.Employees.FirstOrDefault(d => d.Id == newEmployee.Id);
                if (department != null)
                {
                    department.NameOfDepartment = newName;                    
                    department.EmployeesId = newEmployee.Id;
                    employee.DepartmentId = department.Id;
                    db.SaveChanges();
                    result = "Готово! Подразделение " + department.NameOfDepartment + " было изменено";
                }               
            }
            return result;
        }

        //изменить сотрудника
        public static string EditEmployee(Employees oldEmployee, string newName, string newSurname, string newPatronymic, DateTime newDateOfBirth, Enum newGender, Departments newDepartment)
        {
            string result = "NotExist";
            using (ApplicationContext db = new ApplicationContext())
            {

                Employees employee = db.Employees.FirstOrDefault(d => d.Id == oldEmployee.Id);
                if (employee != null)
                {
                    employee.Name = newName;
                    employee.Surname = newSurname;
                    employee.Patronymic = newPatronymic;
                    employee.DateOfBirth = newDateOfBirth;
                    employee.Gender = (Employees.gender)newGender;
                    employee.DepartmentId = newDepartment.Id;
                    db.SaveChanges();
                    result = "Готово! Сотрудник " + employee.Name + ", " + employee.Surname + " был изменен";
                }

            }
            return result;
        }

        //изменить заказ
        public static string EditOrder(Orders oldOrder, int newOrderNumber, string newOrderName, Employees newOrderEmployee, List<string> newTags)
        {
            string result = "NotExist";
            using (ApplicationContext db = new ApplicationContext())
            {
                
                Orders order = db.Orders.FirstOrDefault(d => d.Id == oldOrder.Id);
                if (order != null)
                {
                    order.OrderNumber = newOrderNumber;
                    order.OrderName = newOrderName;
                    order.EmployeeId = newOrderEmployee.Id; 
                    List<string> temp = new List<string>();
                    var orders = db.Orders.Where(c => c.Id == order.Id).Include(c => c.Tags).ToList();
                    foreach (var t in orders)
                    {
                        foreach (Tags s in t.Tags)
                            temp.Add(s.Name);
                    }
                    order.Tags = new List<Tags>();
                    List<string> allTagsList = new List<string>();
                    allTagsList.AddRange(temp);
                    allTagsList.AddRange(newTags);
                    List<string> finaleTagList = allTagsList.Distinct().ToList();

                    for (int i = 0; i < finaleTagList.Count; i++)
                    {
                        Tags tg = db.Tags.FirstOrDefault(t => t.Name == finaleTagList[i]);
                        order.Tags.Add(tg);
                    }
                    db.SaveChanges();
                    result = "Готово! Заказ " + order.OrderName + " был изменен";
                }
            }
            return result;
        }

        //получить сотрудника по id
        public static Employees GetEmployeesId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Employees employees = db.Employees.FirstOrDefault(d => d.Id == id);
                return employees;
            }
        }

        //получить подразделение по id
        public static Departments GetDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Departments departments = db.Departments.FirstOrDefault(d => d.Id == id);
                return departments;
            }
        }

        //получить список тегов для заказа по id
        public static List<string> GetAllTagsByOrder(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {                
                List<string> temp = new List<string>();
                var orders = db.Orders.Where(c => c.Id == id).Include(c => c.Tags).ToList();
                foreach (var t in orders)
                {
                    foreach (Tags s in t.Tags)
                        temp.Add(s.Name);
                }
                return temp;
            }
        }
    }
}
