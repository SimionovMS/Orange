using System.Collections.Generic;
using DataAccess.Models;

namespace Repository
{
    public interface IRepository
    {
        IEnumerable<FavoriteMovie> GetAll();
    }
}