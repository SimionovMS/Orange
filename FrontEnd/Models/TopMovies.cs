using System.Collections.Generic;
using Mv.Integrations;

namespace FrontEnd.Models
{
    public class TopMovies
    {
        public List<MovieApi> Popular { get; set; }
        public List<MovieApi> TopRevenue { get; set; }
        public List<MovieApi> Recent { get; set; }
    }
}