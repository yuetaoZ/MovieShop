using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {

        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel);
         
        Task<UserLoginResponseModel> Login(string email, string password);

        // delete
        // EditUser
        Task<UserProfileResponseModel> EditUserProfile(int userId);
        // Change Password
        // Purchase Movie
        // Favorite Movie
        // Add Review
        // Get All Purchased Movies
        Task<List<MovieCardResponseModel>> GetUserPurchasedMovies(int userId);
        // Get All Favorited Movies
        Task<List<MovieCardResponseModel>> GetUserFavoriteMovies(int userId);
        // Edit Review
        // Remove Favorite
        // Get User Details
        Task<UserProfileResponseModel> GetUserProfile(int userId);
        // 
    }
}
