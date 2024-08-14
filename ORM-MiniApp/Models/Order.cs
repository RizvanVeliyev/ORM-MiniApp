using ORM_MiniApp.Enums;
using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    public class Order : BaseEntity
    {

        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus  Status { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
