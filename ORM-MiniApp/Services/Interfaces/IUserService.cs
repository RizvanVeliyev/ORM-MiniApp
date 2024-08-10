using Microsoft.AspNetCore.Mvc;
using ORM_MiniApp.Dtos.UserDtos;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Services.Interfaces
{
    internal interface IUserService
    {
        //        RegisterUser: Yeni istifadəçi qeydiyyatdan keçirmək.Əgər qeydiyyat məlumatları tam deyilsə, InvalidUserInformationException qaytarılmalıdır.  
        //LoginUser: Istifadəçinin sistemi girişi.
        Task RegisterUser(UserRegDto newUser);
        Task Login(UserLoginDto user);
        Task Update(User user);
        Task<List<Order>> GetUserOrders(int id);
        Task<IActionResult> ExportUserOrdersToExcel(int userId);

        //Əgər istifadəçi adı və ya şifrə yanlışdırsa, UserAuthenticationException qaytarılmalıdır.
        //UpdateUserInfo: Istifadəçi məlumatlarını yeniləmək.

        //Əgər yeniləmək istənilən istifadəçi tapılmazsa, NotFoundException qaytarılmalıdır.
        //GetUserOrders:   Istifadəçi bütün sifarişlərini qaytaracaq
        //ExportUserOrdersToExcel:  Istifadəçi bütün sifarişlərini bir excel fayla export edəcək
    }
}
