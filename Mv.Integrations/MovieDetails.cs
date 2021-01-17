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
        public string ImdbLink { get; set; }
        public string Status { get; set; }
        public string Tagline { get; set; }
        public string Video { get; set; }
        public List<string> ProductionCompanies { get; set; }
        public List<string> ProductionCountries { get; set; }
        public List<string> SpokenLanguages { get; set; }
        public string Collection { get; set; }
        public bool IsFavorite { get; set; }
    }
}