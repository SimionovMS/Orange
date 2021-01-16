using System.Collections.Generic;
using System.Threading.Tasks;
using Mv.Integrations;

namespace Service.Interface
{
    public interface IService
    {
        PagedMovies GetMoviesByPage(int pageNumber);
        MovieDetails GetMovieDetails(long movieId);
    }
}