using System;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mv.Integrations
{
    public interface IApiClient
    {
        IEnumerable<Movie> GetMovies(int pageNumber);
        MovieDetails GetMovieDetails(long movieId);
    }

    public class ApiClient : IApiClient
    {
        private RestRequest _restRequest;
        private readonly RestClient _restClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public ApiClient(IConfiguration configuration)
        {
            _apiKey = configuration.GetSection("MovieDbApiKey").Value;
            _restClient = new RestClient(configuration.GetSection("MovieDbBaseUrl").Value);
            _baseUrl = configuration.GetSection("MovieDbBaseUrl").Value;
        }

        public IEnumerable<Movie> GetMovies(int pageNumber)
        {
            var resource = $"/discover/movie?api_key={_apiKey}&page={pageNumber}";
            _restRequest = new RestRequest {Resource = _baseUrl + resource};
            var response = _restClient.Execute<List<object>>(_restRequest);
            
            if (response.StatusCode != HttpStatusCode.OK) return new List<Movie>();;
            
            var data = JObject.Parse(response.Content);
            var jResults = data["results"];
            var moviesList = jResults.Select(content => new Movie
                {
                    IsAdult = content["adult"].ToString() == "true",
                    BackdropPath = content["backdrop_path"].ToString(),
                    Id = int.Parse(content["id"].ToString()),
                    OriginalLanguage = content["original_language"].ToString(),
                    OriginalTitle = content["original_title"].ToString(),
                    Overview = content["overview"].ToString(),
                    Popularity = decimal.Parse(content["popularity"].ToString()),
                    PosterPath = content["poster_path"].ToString(),
                    ReleaseDate = content["release_date"] != null && !string.IsNullOrEmpty(content["release_date"].ToString())
                    ? DateTime.ParseExact(content["release_date"].ToString(), "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture) : (DateTime?) null,
                    Title = content["title"].ToString(),
                    HasVideo = content["video"].ToString() == "true",
                    VoteAverage = decimal.Parse(content["vote_average"].ToString()),
                    VoteCount = long.Parse(content["vote_count"].ToString()),
                    GenreIds = content["genre_ids"].Values<int>().ToList()
                })
                .ToList();
            
            return moviesList;
        }

        public MovieDetails GetMovieDetails(long movieId)
        {
            var resource = $"/movie/{movieId}?api_key={_apiKey}";
            _restRequest = new RestRequest {Resource = _baseUrl + resource};
            var response = _restClient.Execute<List<object>>(_restRequest);
            
            if (response.StatusCode != HttpStatusCode.OK) return new MovieDetails();
            
            var content = JObject.Parse(response.Content);
            var movie = new MovieDetails
            {
                IsAdult = content["adult"].ToString() == "true",
                BackdropPath = content["backdrop_path"].ToString(),
                Id = int.Parse(content["id"].ToString()),
                OriginalLanguage = content["original_language"].ToString(),
                OriginalTitle = content["original_title"].ToString(),
                Overview = content["overview"].ToString(),
                Popularity = decimal.Parse(content["popularity"].ToString()),
                PosterPath = content["poster_path"].ToString(),
                ReleaseDate = content["release_date"] != null &&
                              !string.IsNullOrEmpty(content["release_date"].ToString())
                    ? DateTime.ParseExact(content["release_date"].ToString(), "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture)
                    : (DateTime?) null,
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
                    .Select(x => int.Parse(x["id"].ToString())).ToList(),
                ProductionContries =
                    content["production_countries"].ToList().Select(x => x["name"].ToString()).ToList(),
                SpokenLanguages = content["spoken_languages"].ToList().Select(x => x["name"].ToString()).ToList(),
                Collection = long.Parse(content["belongs_to_collection"]["id"].ToString())
            };

            return movie;
        }
    }
}