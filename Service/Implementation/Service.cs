using System.Linq;
using Microsoft.Extensions.Configuration;
using Mv.Integrations;
using Service.Interface;

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

        public PagedMovies GetMoviesByPage(int pageNumber, SortBy sortBy)
        {
            return _apiClient.GetMovies(pageNumber, sortBy);
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

            generalDetails.Genres = _apiClient.GetGenres()
                .Where(x => generalDetails.GenreIds.Contains(x.Key))
                .ToDictionary(x => x.Key, x => x.Value);
            generalDetails.ImdbId = string.IsNullOrEmpty(generalDetails.ImdbId) ? "" : _configuration.GetSection("ImdbUrl").Value + generalDetails.ImdbId;

            return generalDetails;
        }
    }
}