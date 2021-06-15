using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
    public class PurchaseResponseModel
    {
        public int UserId { get; set; }
        public List<PurchasedMovieResponseModel> PurchasedMovies { get; set; }
        public class PurchasedMovieResponseModel: MovieCardResponseModel
        {
            public DateTime PurchaseDateTme { get; set; }
        }
    }
}
