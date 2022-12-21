using System.ComponentModel.DataAnnotations.Schema;

namespace test.Model
{
    public class Departments
    {
        public int Id { get; set; }
        public string NameOfDepartment { get; set; }
        public int EmployeesId { get; set; }  
        
        [NotMapped]
        public Employees HeadOfDepartment
        {
            get
            {
                return DataWorker.GetEmployeesId(EmployeesId);
            }
        }
        
    }
}
