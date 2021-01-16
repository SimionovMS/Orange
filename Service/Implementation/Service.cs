using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using Mv.Integrations;

namespace Service.Implementation
{
    public class Service : IService
    {
        private readonly IApiClient _apiClient;
        private readonly IConfiguration _configuration;

        public Service(IApiClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public PagedMovies GetMoviesByPage(int pageNumber)
        {
            return _apiClient.GetMovies(pageNumber);
        }

        public MovieDetails GetMovieDetails(long movieId)
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

            generalDetails.Genres = (Dictionary<int, string>) _apiClient.GetGenres()
                .Where(x => generalDetails.GenreIds.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
            generalDetails.ImdbId = _configuration.GetSection("ImdbUrl").Value + generalDetails.ImdbId;
            
            return generalDetails;
        }
    }
}