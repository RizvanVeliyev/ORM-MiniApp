using ORM_MiniApp.Enums;
using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    internal class Order : BaseEntity
    {
        //        Id: int - Sifarişin təyinatı
        //UserId: int - Sifarişi verən istifadəçinin ID-si
        //OrderDate: DateTime - Sifariş tarixi
        //TotalAmount: decimal - Sifarişin ümumi məbləği
        //Status: OrderStatus Enumı tipində olacaq(Bunun haqqında məlumat aşağıdadır) - Sifarişin statusu

        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus  Status { get; set; }

    }
}
