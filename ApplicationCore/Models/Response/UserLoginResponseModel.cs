using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
   public class UserLoginResponseModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<RoleModel> Roles { get; set; }

    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
