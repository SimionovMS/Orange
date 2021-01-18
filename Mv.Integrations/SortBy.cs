using System;

namespace Mv.Integrations
{
    public class SortBy
    {
        private SortBy(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
        public static SortBy Popularity => new SortBy("vote_count.desc");
        public static SortBy ReleaseDate => new SortBy("release_date.desc");
        public static SortBy Revenue => new SortBy("revenue.desc");
        public static SortBy TopRated => new SortBy("vote_average.desc");
        public static SortBy Latest => new SortBy(ReleaseDate.Value + $"&release_date.lte={DateTime.Now.Date:yyyy-MM-dd}");
    }
}