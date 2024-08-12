using ORM_MiniApp.Dtos.OrderDtos;
using ORM_MiniApp.Exceptions;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Services.Interfaces
{
    internal interface IOrderService
    {   
        Task CreateOrder(OrderDto order);
        Task CancelOrder(int id);

        Task CompleteOrder(int id);

        Task<List<Order>> GetOrders();
        Task AddOrderDetail(OrderDetail orderDetail);

    }
}
