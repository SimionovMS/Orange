using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<FavoriteMovie> GetAll()
        {
            return _dbContext.FavoriteMovies.AsEnumerable();
        }

        public bool IsFavorite(long movieId, string userId)
        {
            return _dbContext.FavoriteMovies.Any(x => x.MovieId == movieId && x.UserId == userId);
        }

        public void DeleteFromFavorite(FavoriteMovie favoriteMovie)
        {
            _dbContext.FavoriteMovies.RemoveRange(_dbContext.FavoriteMovies.Where(x =>
                x.MovieId == favoriteMovie.MovieId && x.UserId == favoriteMovie.UserId));
        }

        public void AddFavorite(FavoriteMovie movie)
        {
            _dbContext.FavoriteMovies.Add(movie);
            _dbContext.SaveChanges();
        }
    }
}