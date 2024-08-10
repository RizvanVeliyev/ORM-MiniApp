using Microsoft.AspNetCore.Mvc;
using ORM_MiniApp.Dtos.UserDtos;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Services.Interfaces
{
    internal interface IUserService
    {
        
        Task RegisterUser(UserRegDto newUser);
        Task Login(UserLoginDto user);
        Task Update(User user);
        Task<List<Order>> GetUserOrders(int id);
        Task<IActionResult> ExportUserOrdersToExcel(int userId);

 
    }
}
