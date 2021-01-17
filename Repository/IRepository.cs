using System.Collections.Generic;
using DataAccess;

namespace Repository
{
    public interface IRepository
    {
        IEnumerable<FavoriteMovie> GetAll();
        void AddFavorite(FavoriteMovie movie);
        bool IsFavorite(long movieId, string userId);
        void DeleteFromFavorite(FavoriteMovie favoriteMovie);
    }
}