using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models.Response
{
    public class UserProfileResponseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Make sure you enter first name is not blank!!")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Make sure you enter last name is not blank!!!!")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please make sure email is not blank")]
        [StringLength(128)]
        [EmailAddress(ErrorMessage = "Please check your format of email address")]
        public string Email { get; set; }
        
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "please make sure dateofbirth is not blank!!")]
        public DateTime? DateOfBirth { get; set; }
        public DateTime? LastLoginDateTime { get; set; }

        public List<MovieCardResponseModel> Purchases { get; set; }
        public List<MovieCardResponseModel> Favorites { get; set; }

    }
}
