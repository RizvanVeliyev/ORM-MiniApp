using ORM_MiniApp.Models;

namespace ORM_MiniApp.Dtos.PaymentDtos
{
    internal class PaymentDto
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }
    }
}
