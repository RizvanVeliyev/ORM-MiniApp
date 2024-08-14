﻿using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    public class Payment : BaseEntity
    {
     

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
