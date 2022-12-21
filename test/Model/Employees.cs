using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Model
{
    public class Employees
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public gender Gender { get; set; }
        public enum gender
        {
            Мужской,
            Женский
        }        
        public int DepartmentId { get; set; }

        [NotMapped]
        public Departments EmployeeInDepartment
        {
            get
            {
                return DataWorker.GetDepartmentId(DepartmentId);
            }
        }
    }
}
