using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel userRegisterRequestModel)
        {
            // first we need to check the email does not exists in our database

            var dbUser = await _userRepository.GetUserByEmail(userRegisterRequestModel.Email);

            if (dbUser != null)
                // email exists in db
                throw new Exception("User already exists, please try to login");

            // generate a unique Salt
            var salt = CreateSalt();

            // hash the password with userRegisterRequestModel.Password + salt from above step
            var hashedPassword = CreateHashedPassword(userRegisterRequestModel.Password, salt);

            // call the user repository to save the user Info

            var user = new User
            {
                FirstName = userRegisterRequestModel.FirstName,
                LastName = userRegisterRequestModel.LastName,
                Email = userRegisterRequestModel.Email,
                DateOfBirth = userRegisterRequestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            var createdUser = await _userRepository.Add(user);

            // convert the returned user entity to UserRegisterResponseModel

            var response = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName,
                Email = createdUser.Email
            };

            return response;
        }
        public async Task<UserLoginResponseModel> Login(string email, string password)
        {
            // go to database and get the user info -- row by email
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                // return null
                return null;
            }

            // get the password from UI and salt from above step from database and call CreateHashedPassword method

            var hashedPassword = CreateHashedPassword(password, user.Salt);

            if (hashedPassword == user.HashedPassword)
            {
                // user entered correct password

                var loginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return loginResponseModel;
            }

            return null;
        }
        public async Task<List<MovieCardResponseModel>> GetUserPurchasedMovies(int id)
        {
            var user = await _userRepository.GetUserById(id);

            var purchedMovieCardList = new List<MovieCardResponseModel>();
            foreach (var usermovie in user.Purchases)
            {
                purchedMovieCardList.Add(new MovieCardResponseModel
                {
                    Id = usermovie.MovieId,
                    PosterUrl = usermovie.Movie.PosterUrl,
                    ReleaseDate = usermovie.Movie.ReleaseDate.GetValueOrDefault(),
                    Title = usermovie.Movie.Title
                });
            }

            return purchedMovieCardList;
        }
        private string CreateSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }
        private string CreateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }

        public async Task<UserProfileResponseModel> GetUserProfile(int userId)
        {

            var user = await _userRepository.GetUserById(userId);
            var userProfile = new UserProfileResponseModel
            {
               Id = user.Id, 
               FirstName = user.FirstName,
               LastName = user.LastName,
               DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
               Email = user.Email,
               PhoneNumber = user.PhoneNumber,
               LastLoginDateTime = user.LastLoginDateTime
            };


            userProfile.Purchases = await GetUserPurchasedMovies(userId);
            userProfile.Favorites = await GetUserFavoriteMovies(userId);

            return userProfile;
        }

        public async Task<List<MovieCardResponseModel>> GetUserFavoriteMovies(int userId)
        {
            var user = await _userRepository.GetUserById(userId);

            var favoriteMovieCardList = new List<MovieCardResponseModel>();
            foreach (var usermovie in user.Favorites)
            {
                favoriteMovieCardList.Add(new MovieCardResponseModel
                {
                    Id = usermovie.MovieId,
                    PosterUrl = usermovie.Movie.PosterUrl,
                    ReleaseDate = usermovie.Movie.ReleaseDate.GetValueOrDefault(),
                    Title = usermovie.Movie.Title
                });
            }

            return favoriteMovieCardList;
        }

        public Task<UserProfileResponseModel> EditUserProfile(int userId)
        {
            throw new NotImplementedException();
        }
    }
}