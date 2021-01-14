using System.Collections.Generic;

namespace Service.Interface
{
    public interface IService
    {
        IEnumerable<object> GetMoviesByPage(int pageNumber);
    }
}