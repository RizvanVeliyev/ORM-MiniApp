﻿using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Interfaces;

namespace ORM_MiniApp.Repositories.Implementations
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
    }
}
