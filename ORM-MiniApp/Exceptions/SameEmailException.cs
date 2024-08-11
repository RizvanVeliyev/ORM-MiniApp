using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MiniApp.Exceptions
{
    internal  sealed class SameEmailException:Exception
    {
        public SameEmailException(string message):base(message)
        {

        }
    }
}
