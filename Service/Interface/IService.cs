using System.Collections.Generic;
using Mv.Integrations;

namespace Service.Interface
{
    public interface IService
    {
        IEnumerable<Mv.Integrations.Movie> GetMoviesByPage(int pageNumber);
        MovieDetails GetMovieDetails(long movieId);
    }
}