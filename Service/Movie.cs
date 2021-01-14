using System;
using System.Collections.Generic;

namespace Service
{
    public class Movie
    {
        public long Id { get; set; }
        public bool IsAdult { get; set; } 
        public string BackdropPath { get; set; } 
        public List<int> GenreIds { get; set; } 
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public long Popularity { get; set; }
        public string PosterPath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }
        public bool HasVideo { get; set; }
        public decimal VoteAverage { get; set; }
        public long VoteCount { get; set; }
    }
}