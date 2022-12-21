using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Model
{
    public class Orders
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public string OrderName { get; set; }        
        public int EmployeeId { get; set; }
        public List<Tags> Tags { get; set; }

        [NotMapped]
        public Employees OrderEmployee
        {
            get
            {
                return DataWorker.GetEmployeesId(EmployeeId);
            }
        }

        [NotMapped]
        public List<string> TagsList
        {
            get
            {
                return DataWorker.GetAllTagsByOrder(Id);
            }
        }
    }
}
