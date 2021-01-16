using Mv.Integrations;

namespace Service.Interface
{
    public interface IService
    {
        PagedMovies GetMoviesByPage(int pageNumber, SortBy sortBy);
        MovieDetails GetMovieDetails(long movieId);
    }
}