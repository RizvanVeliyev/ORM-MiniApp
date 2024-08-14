using ORM_MiniApp.Models;
using ORM_MiniApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MiniApp.Repositories.Implementations
{
    public class UserRepository : Repository<User>, IUserRepository
    {
    }
}
