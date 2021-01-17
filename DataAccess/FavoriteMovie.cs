namespace DataAccess.Models
{
    public class FavoriteMovie
    {
        public long Id { get; set; }
        public long MovieId { get; set; }
        public long UserId { get; set; }
    }
}