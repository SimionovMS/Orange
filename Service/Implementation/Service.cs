using System.Collections.Generic;
using Service.Interface;
using Mv.Integrations;

namespace Service.Implementation
{
    public class Service : IService
    {
        private readonly IApiClient _api;

        public Service(IApiClient api)
        {
            _api = api;
        }

        public IEnumerable<object> GetMoviesByPage(int pageNumber)
        {
            return  _api.GetMovies(pageNumber);
        }
    }
}