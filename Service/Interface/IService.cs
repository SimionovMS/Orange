using Mv.Integrations;

namespace Service.Interface
{
    public interface IService
    {
        PagedMovies GetMoviesByPage(int pageNumber, SortBy sortBy);
        MovieDetails GetMovieDetails(long movieId);
        void AddFavorite(long movieId, string userId);
        void DeleteFromFavorite(long movieId, string userId);
    }
}