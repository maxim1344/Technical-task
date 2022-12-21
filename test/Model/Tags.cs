using System.Collections.Generic;

namespace test.Model
{
    public class Tags
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Orders> OrdersTag { get; set; }       
    }
}
