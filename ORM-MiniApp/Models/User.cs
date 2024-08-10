using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    internal class User : BaseEntity
    {
        //        Id: int - Istifadəçinin təyinatı
        //FullName: string - Istifadəçinin tam adı
        //Email: string - Istifadəçinin e-poçt ünvanı
        //Password: string - Istifadəçinin şifrəsi
        //Address: string - Istifadəçinin ünvanı

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
