using System.Collections.Generic;
using Mv.Integrations;

namespace FrontEnd.Models
{
    public class TopMovies
    {
        public List<Movie> Popular { get; set; }
        public List<Movie> TopRevenue { get; set; }
        public List<Movie> Recent { get; set; }
    }
}