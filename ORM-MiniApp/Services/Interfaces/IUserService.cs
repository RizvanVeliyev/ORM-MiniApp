using ORM_MiniApp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MiniApp.Services.Interfaces
{
    internal interface IUserService
    {
        //        RegisterUser: Yeni istifadəçi qeydiyyatdan keçirmək.Əgər qeydiyyat məlumatları tam deyilsə, InvalidUserInformationException qaytarılmalıdır.  
        //LoginUser: Istifadəçinin sistemi girişi.
        Task RegisterUser();

        //Əgər istifadəçi adı və ya şifrə yanlışdırsa, UserAuthenticationException qaytarılmalıdır.
        //UpdateUserInfo: Istifadəçi məlumatlarını yeniləmək.

        //Əgər yeniləmək istənilən istifadəçi tapılmazsa, NotFoundException qaytarılmalıdır.
        //GetUserOrders:   Istifadəçi bütün sifarişlərini qaytaracaq
        //ExportUserOrdersToExcel:  Istifadəçi bütün sifarişlərini bir excel fayla export edəcək
    }
}
