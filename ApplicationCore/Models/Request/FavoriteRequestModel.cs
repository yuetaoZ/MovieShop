using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Request
{
    public class FavoriteRequestModel
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}
