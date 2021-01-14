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

            var moviesList = jResults.Select(jResult => new Movie
                {
                    IsAdult = jResult["adult"].ToString() == "true",
                    BackdropPath = jResult["backdrop_path"].ToString(),
                    GenreIds = jResult["genre_ids"].Values<int>().ToList(),
                    Id = int.Parse(jResult["id"].ToString()),
                    OriginalLanguage = jResult["original_language"].ToString(),
                    OriginalTitle = jResult["original_title"].ToString(),
                    Overview = jResult["overview"].ToString(),
                    Popularity = decimal.Parse(jResult["popularity"].ToString()),
                    PosterPath = jResult["poster_path"].ToString(),
                    ReleaseDate = jResult["release_date"] != null && !string.IsNullOrEmpty(jResult["release_date"].ToString())
                    ? DateTime.ParseExact(jResult["release_date"].ToString(), "yyyy-MM-dd",
                        System.Globalization.CultureInfo.InvariantCulture) : (DateTime?) null,
                    Title = jResult["title"].ToString(),
                    HasVideo = jResult["video"].ToString() == "true",
                    VoteAverage = decimal.Parse(jResult["vote_average"].ToString()),
                    VoteCount = long.Parse(jResult["vote_count"].ToString())
                })
                .ToList();
            
            // if (response.Data.Count <= 0) return moviesList;
            //
            // moviesList.AddRange(GetMovies(pageNumber + 1));

            return moviesList;
        }
    }
}