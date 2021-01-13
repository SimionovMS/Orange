using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class Repository : IRepository
    {
        private readonly ApplicationContext context;

        public Repository(ApplicationContext context)
        {
            this.context = context;
        }

        IEnumerable<FavoriteMovie> IRepository.GetAll()
        {
            return context.FavoriteMovies.AsEnumerable();
        }
    }
}
