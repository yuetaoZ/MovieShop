using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
    public class FavoriteResponseModel
    {
        public int UserId { get; set; }
        public List<MovieCardResponseModel> FavoriteMovies { get; set; }
    }
}
