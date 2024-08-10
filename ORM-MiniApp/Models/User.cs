using ORM_MiniApp.Models.Common;

namespace ORM_MiniApp.Models
{
    internal class User : BaseEntity
    {

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}
