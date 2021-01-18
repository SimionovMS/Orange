using System.Collections.Generic;

namespace Mv.Integrations
{
    public class PagedMovies
    {
        public List<MovieApi> Movies { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }

        public bool ShowPrevious => Page > 1;
        public bool ShowNext => Page < TotalPages;
    }
}