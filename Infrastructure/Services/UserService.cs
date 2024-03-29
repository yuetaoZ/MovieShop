﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
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
        private readonly ICurrentUserService _currentUserService;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMovieService _movieService;
        private readonly IAsyncRepository<Favorite> _favoriteRepository;
        public UserService(IUserRepository userRepository, ICurrentUserService currentUserService, IPurchaseRepository purchaseRepository, IMovieService movieService, IAsyncRepository<Favorite> favoriteRepository)
        {
            _userRepository = userRepository;
            _currentUserService = currentUserService;
            _purchaseRepository = purchaseRepository;
            _movieService = movieService;
            _favoriteRepository = favoriteRepository;
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

            var createdUser = await _userRepository.AddAsync(user);

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
            var user = await _userRepository.GetByIdAsync(id);

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

            var user = await _userRepository.GetByIdAsync(userId);
            var userProfile = new UserProfileResponseModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                LastLoginDateTime = user.LastLoginDateTime.GetValueOrDefault(DateTime.Now)
            };


            userProfile.Purchases = await GetUserPurchasedMovies(userId);
            userProfile.Favorites = await GetUserFavoriteMovies(userId);

            return userProfile;
        }

        public async Task<List<MovieCardResponseModel>> GetUserFavoriteMovies(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

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

        public async Task<UserProfileResponseModel> EditUserProfile(UserProfileResponseModel userProfileResponseModel)
        {        
            var user = await _userRepository.GetByIdAsync(userProfileResponseModel.Id);

            if (user == null)
            {
                // return null
                return null;
            }

            user.Id = userProfileResponseModel.Id;
            user.FirstName = userProfileResponseModel.FirstName;
            user.LastName = userProfileResponseModel.LastName;
            user.Email = userProfileResponseModel.Email;
            user.PhoneNumber = userProfileResponseModel.PhoneNumber;
            user.LastLoginDateTime = userProfileResponseModel.LastLoginDateTime;

            await _userRepository.UpdateAsync(user);

            var response = new UserProfileResponseModel
            {
                Id = userProfileResponseModel.Id,
                FirstName = userProfileResponseModel.FirstName,
                LastName = userProfileResponseModel.LastName,
                Email = userProfileResponseModel.Email,
                PhoneNumber = userProfileResponseModel.PhoneNumber,
                LastLoginDateTime = userProfileResponseModel.LastLoginDateTime
            };

            return response;
        }

        public Task<UserLoginResponseModel> validateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<UserRegisterResponseModel> GetUserDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Uri> UploadUserProfilePicture(UserProfileRequestModel model)
        {
            throw new NotImplementedException();
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequestModel)
        {
            if (_currentUserService.UserId != favoriteRequestModel.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to add favorite.");
            if (await FavoriteExists(favoriteRequestModel.UserId, favoriteRequestModel.MovieId))
                throw new ConflictException("Movie already favorited");

            var favorite = new Favorite()
            {
                MovieId = favoriteRequestModel.MovieId,
                UserId = favoriteRequestModel.UserId
            };

            await _favoriteRepository.AddAsync(favorite);
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequestModel)
        {
            if (_currentUserService.UserId != favoriteRequestModel.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to remove favorite.");
            if (!await FavoriteExists(favoriteRequestModel.UserId, favoriteRequestModel.MovieId))
                throw new NotFoundException("Movie not found", favoriteRequestModel.MovieId);

            var dbFavorite = await _favoriteRepository.ListAsync(f => f.MovieId == favoriteRequestModel.MovieId && 
                                                                      f.UserId == favoriteRequestModel.UserId);
            await _favoriteRepository.DeleteAsync(dbFavorite.First());
        }

        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            return await _favoriteRepository.GetExistsAsync(f => f.UserId == id && f.MovieId == movieId);
        }

        public async Task PurchaseMovie(PurchaseRequestModel purchaseRequest)
        {
            if (_currentUserService.UserId != purchaseRequest.UserId)
                throw new HttpException(HttpStatusCode.Unauthorized, "You are not Authorized to purchase");
            if (await IsMoviePurchased(purchaseRequest))
                throw new ConflictException("Movie already Purchase");

            var movieDetail = await _movieService.GetMovieDetailsById(purchaseRequest.MovieId);
            purchaseRequest.TotalPrice = movieDetail.Price;

            var purchase = new Purchase()
            {
                UserId = purchaseRequest.UserId,
                TotalPrice = movieDetail.Price.GetValueOrDefault(),
                PurchaseDateTime = purchaseRequest.PurchaseDateTime.GetValueOrDefault(),
                PurchaseNumber = purchaseRequest.PurchaseNumber.GetValueOrDefault(),
                MovieId = purchaseRequest.MovieId
            };
            await _purchaseRepository.AddAsync(purchase);
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest)
        {
            return await _purchaseRepository.GetExistsAsync(p => p.UserId == purchaseRequest.UserId
            && p.MovieId == purchaseRequest.MovieId);
        }

        public Task AddMovieReview(ReviewRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieReview(ReviewRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewResponseModel> GetAllReviewsByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileResponseModel> GetUserProfile(int? userId)
        {
            throw new NotImplementedException();
        }
    }
}