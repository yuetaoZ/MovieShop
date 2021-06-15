using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models.Response
{
    public class ReviewResponseModel
    {
        public int UserId { get; set; }
        public List<MovieReviewResponseModel> MovieReviews { get; set; }
    }
}
