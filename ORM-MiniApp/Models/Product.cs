using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    internal class Product : BaseEntity
    {
        //Id: int - Məhsulun təyinatı
        //Name: string - Məhsulun adı
        //Price: decimal - Məhsulun qiyməti
        ////Stock: int - Məhsulun anbarda qalan sayı
        //Description: string - Məhsul haqqında əlavə məlumat
        //CreatedDate - məhsulun yaranma tarixi
        //UpdatedDate - məhsulun update olunma tarixi

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get;}
        public string Description { get; set; }
        public DateTime CreatedAt { get; set;}
        public DateTime UpdatedAt { get; set;}
    }
}
