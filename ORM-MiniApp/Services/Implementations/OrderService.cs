using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.OrderDtos;
using ORM_MiniApp.Enums;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Services.Interfaces;
using System.Threading.Tasks;

namespace ORM_MiniApp.Services.Implementations
{
    internal class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        public OrderService()
        {
            _context= new AppDbContext();
        }
        public async Task CreateOrder(OrderDto order)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == order.UserId);

            if (order.TotalAmount < 0 || user == null)
                throw new InvalidOrderException("this order hasn't user or totalamount is less than 0!");
            var orderdto = new Order()
            {
                UserId= order.UserId,
                TotalAmount= order.TotalAmount,
                Status=OrderStatus.Pending
            };
            await _context.Orders.AddAsync(orderdto);
            await _context.SaveChangesAsync();
        }
        public async Task CancelOrder(int id)
        {
            var order=await _context.Orders.FirstOrDefaultAsync(o=>o.Id==id);
            if (order == null)
                throw new NotFoundException($"Cant found order with id:{id}");
            if (order.Status == OrderStatus.Cancelled)
                throw new OrderAlreadyCancelledException("Order already cancelled!");
            order.Status = OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
        }
        public async Task CompleteOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
                throw new NotFoundException($"Cant found order with id:{id}");
            if (order.Status == OrderStatus.Completed)
                throw new OrderAlreadyCompletedException("Order already Completed!");
            order.Status = OrderStatus.Completed;
            await _context.SaveChangesAsync();
        }
        public async Task<List<Order>> GetOrders()
        {
            var orders=await _context.Orders.AsNoTracking().Include(o=>o.User).ToListAsync();
            return orders;
        }
        public Task AddOrderDetail(int id)
        {
            throw new NotImplementedException();
        }

    }
}
