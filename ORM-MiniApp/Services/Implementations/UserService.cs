using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.UserDtos;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    internal class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService()
        {
            _context= new AppDbContext();
        }
        public async Task RegisterUser(UserRegDto newUser)
        {
            if (string.IsNullOrEmpty(newUser.Email) || string.IsNullOrEmpty(newUser.Password) ||
                string.IsNullOrEmpty(newUser.FullName) || string.IsNullOrEmpty(newUser.Address))
                throw new InvalidUserInformationException("All information should entered for register!");
            var user = new User()
            {
                FullName = newUser.FullName,
                Email = newUser.Email,
                Password = newUser.Password,
                Address = newUser.Address
            };
            var users = await _context.Users.ToListAsync();
            foreach (var item in users)
            {
                if (item.Email == user.Email)
                    throw new SameEmailException("This email already exist!");
            }
            
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }
        public async Task Login(UserLoginDto user)
        {
            var userDto = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.FullName== user.FullName && u.Password==user.Password);
            if (userDto == null)
                throw new UserAuthenticationException("Fullname or Password is wrong!");
            Console.WriteLine("You Login Successfully!");

        }

        public async Task<List<Order>> GetUserOrders(int id)
        {
            var user=await _context.Users.AsNoTracking().FirstOrDefaultAsync(u=>u.Id==id);
            if (user == null)
                throw new NotFoundException($"User not found with id:{id}");
            var orders = await _context.Orders.AsNoTracking().Where(o=>o.UserId==id).Include(o=>o.User).ToListAsync();
            if (orders.Count == 0)
                Console.WriteLine("This user Hasn't orders!");
            
            return orders;
        }

        

       

        public async Task Update(User user)
        {
            var userUp = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);
            if (userUp == null)
                throw new NotFoundException($"User not found with id:{user.Id}");
            var userDto = new User()
            {
                Id = user.Id,
                FullName = user.FullName,
                Password = user.Password,
                Address = user.Address,
                Email = user.Email
            };
            _context.Users.Update(userDto);
            await _context.SaveChangesAsync();

        }
        public async Task<IActionResult> ExportUserOrdersToExcel(int userId)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .ToListAsync();

            if (orders.Count == 0)
            {
                throw new NotFoundException($"No orders found for user with id: {userId}");
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Orders");

            worksheet.Cell(1, 1).Value = "Order ID";
            worksheet.Cell(1, 2).Value = "Total Amount";
            worksheet.Cell(1, 3).Value = "Status";

            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                worksheet.Cell(i + 2, 1).Value = order.Id;
                worksheet.Cell(i + 2, 2).Value = order.TotalAmount;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            var fileName = $"UserOrders_{userId}.xlsx";
            return null;
           /* return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName)*/;
        }

    }
}
