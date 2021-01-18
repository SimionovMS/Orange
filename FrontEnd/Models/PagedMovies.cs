using System.Collections.Generic;
using Mv.Integrations;

namespace FrontEnd.Models
{
    public class PagedMovies
    {
        public List<MovieApi> Movies { get; set; }

        public int Page { get; set; }
        public int TotalPages { get; set; }

        public bool ShowPrevious => Page > 1;
        public bool ShowNext => Page < TotalPages;
    }
}