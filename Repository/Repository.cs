using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext _context;

        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        IEnumerable<FavoriteMovie> IRepository.GetAll()
        {
            return _context.FavoriteMovies.AsEnumerable();
        }
    }
}
