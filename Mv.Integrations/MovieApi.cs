﻿using System.Collections.Generic;

namespace Mv.Integrations
{
    public class MovieApi
    {
        public long Id { get; set; }
        public bool IsAdult { get; set; }
        public string BackdropPath { get; set; }
        public List<int> GenreIds { get; set; }
        public Dictionary<int, string> Genres { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalTitle { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }
        public string PosterPath { get; set; }
        public string ReleaseDate { get; set; }
        public string Title { get; set; }
        public bool HasVideo { get; set; }
        public decimal VoteAverage { get; set; }
        public string Collection { get; set; }
        public long VoteCount { get; set; }
    }
}