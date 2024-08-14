using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Contexts;
using ORM_MiniApp.Dtos.OrderDtos;
using ORM_MiniApp.Enums;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Implementations;
using ORM_MiniApp.Repositories.Interfaces;
using ORM_MiniApp.Services.Interfaces;

namespace ORM_MiniApp.Services.Implementations
{
    
    internal class OrderService : IOrderService
    {
        
        private readonly IOrderRepository _orderRepository;
        public OrderService()
        {
            _orderRepository = new  OrderRepository();
        }
        public async Task CreateOrder(OrderDto order)
        {
            IUserRepository userRepository = new UserRepository();
            var user = await userRepository.GetSingleAsync(u => u.Id == order.UserId);

            if (order.TotalAmount < 0 || user == null)
                throw new InvalidOrderException("this order hasn't user or totalamount is less than 0!");
            var orderdto = new Order()
            {
                UserId= order.UserId,
                TotalAmount= order.TotalAmount,
                Status=OrderStatus.Pending
            };
            await _orderRepository.CreateAsync(orderdto);
            await _orderRepository.SaveChangesAsync();
        }
        public async Task CancelOrder(int id)
        {
            var order=await _orderRepository.GetSingleAsync(o=>o.Id==id);
            if (order == null)
                throw new NotFoundException($"Cant found order with id:{id}");
            if (order.Status == OrderStatus.Cancelled)
                throw new OrderAlreadyCancelledException("Order already cancelled!");
            if (order.Status != OrderStatus.Completed)
            {
                order.Status = OrderStatus.Cancelled;
                Console.WriteLine("Order cancelled!");
                _orderRepository.Delete(order);
            }
                
            else Console.WriteLine("Can't cancel completed order!");
            

            await _orderRepository.SaveChangesAsync();
        }
        public async Task CompleteOrder(int id)
        {
            var order = await _orderRepository.GetSingleAsync(o => o.Id == id);
            if (order == null)
                throw new NotFoundException($"Cant found order with id:{id}");
            if (order.Status == OrderStatus.Completed)
                throw new OrderAlreadyCompletedException("Order already Completed!");
            order.Status = OrderStatus.Completed;
            await _orderRepository.SaveChangesAsync();
        }
        public async Task<List<Order>> GetOrders()
        {
            var orders = await _orderRepository.GetAllAsync("User");
            return orders;
        }
        public async Task AddOrderDetail(OrderDetail Od)
        {
            IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
            var order = await _orderRepository.GetSingleAsync(o => o.Id == Od.OrderId);
            if (order == null)
                throw new NotFoundException($"Cant find order with id:{Od.OrderId}");
            var product = await _orderRepository.GetSingleAsync(o => o.Id == Od.ProductId);
            if (product == null)
                throw new NotFoundException($"Cant find order with id:{Od.ProductId}");
            if (Od.Quantity <=0 || Od.PricePerItem <= 0)
                throw new InvalidOrderDetailException("Quantity and PricePerItem must be greater than 0!");
            await orderDetailRepository.CreateAsync(Od);
            await _orderRepository.SaveChangesAsync();
        }

    }
}
