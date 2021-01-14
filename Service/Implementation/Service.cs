using System.Collections.Generic;
using System.Linq;
using Service.Interface;
using Mv.Integrations;

namespace Service.Implementation
{
    public class Service : IService
    {
        private readonly IApiClient _apiClient;

        public Service(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<Mv.Integrations.Movie> GetMoviesByPage(int pageNumber)
        {
            return _apiClient.GetMovies(pageNumber).ToList();
        }

        public MovieDetails GetMovieDetails(long movieId)
        {
            return _apiClient.GetMovieDetails(movieId);
        }
    }
}