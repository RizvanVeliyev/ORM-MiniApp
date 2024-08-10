using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    internal class Payment : BaseEntity
    {
        //        Id: int - Ödənişin təyinatı
        //OrderId: int - Ödəniş edilən sifarişin ID-si
        //Amount: decimal - Ödəniş məbləği
        //PaymentDate: DateTime - Ödəniş tarixi

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
