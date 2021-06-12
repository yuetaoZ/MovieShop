using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
    public class UserProfileResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public List<MovieCardResponseModel> Purchases { get; set; }
        public List<MovieCardResponseModel> Favorites { get; set; }

    }
}
