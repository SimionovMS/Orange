using DataAccess.Models;
using System.Collections.Generic;

namespace Repository
{
    public interface IRepository
    {
        IEnumerable<FavoriteMovie> GetAll();
    }
}
