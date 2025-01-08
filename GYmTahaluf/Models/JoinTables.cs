using GYmTahaluf.Models;

namespace restruarntmvc.Models
{
    public class JoinTables
    {
        public Category Category { get; set; }
        public Product Product { get; set; }
        public ProductCustomer ProductCustomer { get; set; }
        public Customer Customer { get; set; }
    }
}
