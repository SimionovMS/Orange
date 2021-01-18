using System.Collections.Generic;
using DataAccess;

namespace Repository
{
    public interface IRepository
    {
        IEnumerable<FavoriteMovie> GetAll();
        IEnumerable<FavoriteMovie> GetFavoritesByUserId(string userId);
        void AddFavorite(FavoriteMovie movie);
        FavoriteMovie GetFavorite(long movieId, string userId);
        void DeleteFavorite(FavoriteMovie favoriteMovie);
        void Save();
    }
}