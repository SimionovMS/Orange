namespace DataAccess.Models
{
    public class FavoriteMovie
    {
        public long Id { get; set; }
        public string ImdbId { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
    }
}