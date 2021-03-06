﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mv.Integrations;
using Repository;
using Service.Interface;

namespace Service.Implementation
{
    public class Service : IService
    {
        private readonly IApiClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;
        private readonly string _userId;

        public Service(IApiClient apiClient, IConfiguration configuration, IRepository repository,
            IHttpContextAccessor httpContextAccessor)
        {
            _apiClient = apiClient;
            _configuration = configuration;
            _repository = repository;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public PagedMovies GetMoviesByPage(int pageNumber, SortBy sortBy)
        {
            return _apiClient.GetMovies(pageNumber, sortBy);
        }

        public MovieApiDetails GetMovieFullDetails(long movieId)
        {
            var generalDetails = GetMovieDetails(movieId);

            if (_userId != null)
            {
                generalDetails.IsFavorite = _repository.GetFavorite(generalDetails.Id, _userId) != null;
            }

            return generalDetails;
        }

        public IEnumerable<MovieApiDetails> GetFavoriteMovies()
        {
            return _repository.GetFavoritesByUserId(_userId).Select(x => GetMovieDetails(x.MovieId));
        }

        public void AddFavorite(long movieId, string userId)
        {
            _repository.AddFavorite(new FavoriteMovie
            {
                MovieId = movieId,
                UserId = userId
            });
        }

        public void DeleteFromFavorite(long movieId, string userId)
        {
            _repository.DeleteFavorite(new FavoriteMovie
            {
                MovieId = movieId,
                UserId = userId
            });
        }

        private MovieApiDetails GetMovieDetails(long movieId)
        {
            var generalDetails = _apiClient.GetMovieDetails(movieId);
            var videoId = _apiClient.GetVideoId(movieId);
            var imageConfiguration = _apiClient.GetImageConfiguration();

            if (string.IsNullOrEmpty(videoId))
            {
                generalDetails.BackdropPath = imageConfiguration.BaseUrl + imageConfiguration.BackDropSize +
                                              generalDetails.BackdropPath;
            }
            else
            {
                generalDetails.PosterPath = imageConfiguration.BaseUrl + imageConfiguration.PosterSize +
                                            generalDetails.PosterPath;
                generalDetails.Video = _configuration.GetSection("YTUrl").Value + videoId;
            }

            generalDetails.Genres = _apiClient.GetGenres()
                .Where(x => generalDetails.GenreIds.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
            
            generalDetails.ImdbId = string.IsNullOrEmpty(generalDetails.ImdbId)
                ? ""
                : _configuration.GetSection("ImdbUrl").Value + generalDetails.ImdbId;

            return generalDetails;
        }
    }
}