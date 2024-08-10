using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    internal class OrderDetail : BaseEntity
    {
        //        Id: int - Sifariş detallarının təyinatı.
        //OrderId: int - Bağlı olduğu sifarişin ID-si.
        //ProductId: int - Sifariş edilmiş məhsulun ID-si.
        //Quantity: int - Sifariş edilmiş məhsulun sayı.
        //PricePerItem: decimal - Məhsulun hər biri üçün qiymət.

        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerItem { get; set; }


    }
}
