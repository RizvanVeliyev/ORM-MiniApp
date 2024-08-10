using ORM_MiniApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MiniApp.Dtos.OrderDtos
{
    internal class OrderDto
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
    }
}
