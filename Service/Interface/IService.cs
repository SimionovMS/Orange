using System.Collections.Generic;
using Mv.Integrations;

namespace Service.Interface
{
    public interface IService
    {
        PagedMovies GetMoviesByPage(int pageNumber, SortBy sortBy);
        MovieApiDetails GetMovieFullDetails(long movieId);
        IEnumerable<MovieApiDetails> GetFavoriteMovies();
        void AddFavorite(long movieId, string userId);
        void DeleteFromFavorite(long movieId, string userId);
    }
}