using System.Collections.Generic;

namespace Mv.Integrations
{
    public class MovieDetails : Movie
    {
        public decimal Budget { get; set; }
        public decimal Revenue { get; set; }
        public decimal RunTime { get; set; }
        public string Homepage { get; set; }
        public string ImdbId { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public List<int> ProductionCompanies { get; set; }
        public List<string> ProductionContries { get; set; }
        public List<string> SpokenLanguages { get; set; }
        public long Collection { get; set; }
    }
}