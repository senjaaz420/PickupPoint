using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDDiplom.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Secondname { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Experience { get; set; }
        public int? RoleId { get; set; }
        public int? WorkPlaceId { get; set; }
        public Role Role { get; set; }
        public WorkPlace WorkPlace { get; set; }
        public enum Roles
        {
            Admin = 1,
            User = 2

        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
