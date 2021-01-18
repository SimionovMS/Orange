using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Mv.Integrations
{
    public interface IApiClient
    {
        PagedMovies GetMovies(int pageNumber, SortBy sortBy);
        MovieApiDetails GetMovieDetails(long movieId);
        ImageConfiguration GetImageConfiguration();
        string GetVideoId(long movieId);
        Dictionary<int, string> GetGenres();
    }

    public class ApiClient : IApiClient
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly RestClient _restClient;
        private RestRequest _restRequest;

        public ApiClient(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("MovieDbApiKey").Value;
            _restClient = new RestClient(configuration.GetSection("MovieDbBaseUrl").Value);
            _baseUrl = configuration.GetSection("MovieDbBaseUrl").Value;
        }

        public PagedMovies GetMovies(int pageNumber, SortBy sortBy)
        {
            var resource = $"/discover/movie?api_key={_apiKey}&page={pageNumber}&sort_by={sortBy.Value}";
            _restRequest = new RestRequest {Resource = _baseUrl + resource};
            var response = _restClient.Execute<List<object>>(_restRequest);

            if (response.StatusCode != HttpStatusCode.OK) return new PagedMovies();

            var data = JObject.Parse(response.Content);
            var jResults = data["results"];
            var moviesList = jResults.Select(content => new MovieApi
                {
                    IsAdult = content["adult"].ToString() == "true",
                    BackdropPath = content["backdrop_path"].ToString(),
                    Id = int.Parse(content["id"].ToString()),
                    OriginalLanguage = content["original_language"].ToString(),
                    OriginalTitle = content["original_title"].ToString(),
                    Overview = content["overview"].ToString(),
                    Popularity = decimal.Parse(content["popularity"].ToString()),
                    PosterPath = content["poster_path"].ToString(),
                    ReleaseDate = content["release_date"]?.ToString(),
                    Title = content["title"].ToString(),
                    HasVideo = content["video"].ToString() == "true",
                    VoteAverage = decimal.Parse(content["vote_average"].ToString()),
                    VoteCount = long.Parse(content["vote_count"].ToString()),
                    GenreIds = content["genre_ids"].Values<int>().ToList(),
                    Collection = content["belongs_to_collection"] != null ? content["belongs_to_collection"]["name"].ToString() : string.Empty
                }).ToList();

            var pagedMovies = new PagedMovies
            {
                Page = int.Parse(data["page"].ToString()),
                TotalPages = int.Parse(data["total_pages"].ToString()),
                TotalResults = int.Parse(data["total_results"].ToString()),
                Movies = moviesList
            };

            return pagedMovies;
        }

        public MovieApiDetails GetMovieDetails(long movieId)
        {
            var resource = $"/movie/{movieId}?api_key={_apiKey}";
            _restRequest = new RestRequest {Resource = _baseUrl + resource};
            var response = _restClient.Execute<List<object>>(_restRequest);

            if (response.StatusCode != HttpStatusCode.OK) return new MovieApiDetails();

            var content = JObject.Parse(response.Content);
            var movie = new MovieApiDetails
            {
                IsAdult = content["adult"].ToString() == "true",
                BackdropPath = content["backdrop_path"].ToString(),
                Id = long.Parse(content["id"].ToString()),
                OriginalLanguage = content["original_language"].ToString(),
                OriginalTitle = content["original_title"].ToString(),
                Overview = content["overview"].ToString(),
                Popularity = decimal.Parse(content["popularity"].ToString()),
                PosterPath = content["poster_path"].ToString(),
                ReleaseDate = content["release_date"].ToString(),
                Title = content["title"].ToString(),
                HasVideo = content["video"].ToString() == "true",
                VoteAverage = decimal.Parse(content["vote_average"].ToString()),
                VoteCount = long.Parse(content["vote_count"].ToString()),
                GenreIds = content["genres"].ToList().Select(x => int.Parse(x["id"].ToString())).ToList(),
                Budget = decimal.Parse(content["budget"].ToString()),
                Homepage = content["homepage"].ToString(),
                Revenue = decimal.Parse(content["revenue"].ToString()),
                RunTime = decimal.Parse(content["runtime"].ToString()),
                Status = content["status"].ToString(),
                ImdbId = content["imdb_id"].ToString(),
                Tagline = content["tagline"].ToString(),
                ProductionCompanies = content["production_companies"].ToList()
                    .Select(x => x["name"].ToString()).ToList(),
                ProductionCountries =
                    content["production_countries"].ToList().Select(x => x["name"].ToString()).ToList(),
                SpokenLanguages = content["spoken_languages"].ToList().Select(x => x["name"].ToString()).ToList(),
                Collection = content["belongs_to_collection"].Type != JTokenType.Null ? content["belongs_to_collection"]["name"].ToString() : string.Empty
            };

            return movie;
        }

        public ImageConfiguration GetImageConfiguration()
        {
            var imageConf = new ImageConfiguration();
            var resource = $"/configuration?api_key={_apiKey}";
            _restRequest = new RestRequest {Resource = _baseUrl + resource};
            var response = _restClient.Execute<List<object>>(_restRequest);

            if (response.StatusCode != HttpStatusCode.OK) return imageConf;

            var content = JObject.Parse(response.Content);
            imageConf.BaseUrl = content["images"]["secure_base_url"].ToString();
            imageConf.PosterSize = content["images"]["poster_sizes"][3].ToString();
            imageConf.BackDropSize = content["images"]["backdrop_sizes"][2].ToString();

            return imageConf;
        }

        public string GetVideoId(long movieId)
        {
            var resource = $"/movie/{movieId}/videos?api_key={_apiKey}&language=en-US";
            _restRequest = new RestRequest {Resource = _baseUrl + resource};
            var response = _restClient.Execute<List<object>>(_restRequest);

            if (response.StatusCode != HttpStatusCode.OK) return string.Empty;

            var content = JObject.Parse(response.Content);
            var hasVideo = content["results"].Any();

            return hasVideo ? content["results"][0]["key"].ToString() : string.Empty;
        }

        public Dictionary<int, string> GetGenres()
        {
            var resource = $"/genre/movie/list?api_key={_apiKey}&language=en-US";
            _restRequest = new RestRequest {Resource = _baseUrl + resource};
            var response = _restClient.Execute<List<object>>(_restRequest);

            if (response.StatusCode != HttpStatusCode.OK) return new Dictionary<int, string>();

            var content = JObject.Parse(response.Content)["genres"].ToList();
            var dict = content.ToDictionary(genre => int.Parse(genre["id"].ToString()),
                genre => genre["name"].ToString());

            return dict;
        }
    }
}