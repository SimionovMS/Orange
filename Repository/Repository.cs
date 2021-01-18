using System.Collections.Generic;
using System.Linq;
using DataAccess;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<FavoriteMovie> GetFavoritesByUserId(string userId)
        {
            return _dbContext.FavoriteMovies.Where(x => x.UserId == userId);
        }

        public FavoriteMovie GetFavorite(long movieId, string userId)
        {
            return _dbContext.FavoriteMovies.FirstOrDefault(x => x.MovieId == movieId && x.UserId == userId);
        }

        public void DeleteFavorite(FavoriteMovie favoriteMovie)
        {
            _dbContext.FavoriteMovies.RemoveRange(_dbContext.FavoriteMovies.Where(x =>
                x.MovieId == favoriteMovie.MovieId && x.UserId == favoriteMovie.UserId));
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void AddFavorite(FavoriteMovie movie)
        {
            _dbContext.FavoriteMovies.Add(movie);
            Save();
        }
    }
}