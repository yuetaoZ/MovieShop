using ApplicationCore.Entities;
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

        Task<UserLoginResponseModel> validateUser(string email, string password);
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel);
        Task<UserRegisterResponseModel> GetUserDetails(int id);
        Task<User> GetUser(string email);
        Task<Uri> UploadUserProfilePicture(UserProfileRequestModel model);
        Task AddFavorite(FavoriteRequestModel model);
        Task RemoveFavorite(FavoriteRequestModel model);
        Task<bool> FavoriteExists(int id, int movieId);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);
        Task PurchaseMovie(PurchaseRequestModel model);
        Task<bool> IsMoviePurchased(PurchaseRequestModel model);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);
        Task<UserLoginResponseModel> Login(string email, string password);
        Task<UserProfileResponseModel> EditUserProfile(UserProfileResponseModel userProfileResponseModel);
        Task<List<MovieCardResponseModel>> GetUserPurchasedMovies(int userId);
        Task<List<MovieCardResponseModel>> GetUserFavoriteMovies(int userId);
        Task<UserProfileResponseModel> GetUserProfile(int? userId);
        Task AddMovieReview(ReviewRequestModel model);
        Task UpdateMovieReview(ReviewRequestModel model);
        Task DeleteMovieReview(int userId, int movieId);
        Task<ReviewResponseModel> GetAllReviewsByUserId(int id);
    }
}
