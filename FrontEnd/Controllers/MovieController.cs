using System.Collections.Generic;
using System.Linq;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Mv.Integrations;
using Newtonsoft.Json;
using Service.Interface;
using MovieDetails = FrontEnd.Models.MovieDetails;
using PagedMovies = FrontEnd.Models.PagedMovies;

namespace FrontEnd.Controllers
{
    [Route("")]
    [Route("movie")]
    public class MovieController : Controller
    {
        private readonly IService _service;

        public MovieController(IService service)
        {
            _service = service;
        }

        [Route("")]
        [Route("tops")]
        public IActionResult Index()
        {
            return View(new TopMovies
            {
                Popular = JsonConvert.DeserializeObject<List<Movie>>(
                    JsonConvert.SerializeObject(_service.GetMoviesByPage(1, SortBy.Popularity).Movies.Take(10))),
                TopRevenue = JsonConvert.DeserializeObject<List<Movie>>(
                    JsonConvert.SerializeObject(_service.GetMoviesByPage(1, SortBy.Revenue).Movies.Take(10))),
                Recent = JsonConvert.DeserializeObject<List<Movie>>(
                    JsonConvert.SerializeObject(_service.GetMoviesByPage(1, SortBy.ReleaseDate).Movies.Take(10)))
            });
        }


        [Route("popular/{pageNumber:int}")]
        public IActionResult Popular(int pageNumber)
        {
            return View(JsonConvert.DeserializeObject<PagedMovies>(
                JsonConvert.SerializeObject(_service.GetMoviesByPage(pageNumber, SortBy.Popularity))));
        }

        [Route("top-rated/{pageNumber:int}")]
        public IActionResult TopRated(int pageNumber)
        {
            return View(JsonConvert.DeserializeObject<PagedMovies>(
                JsonConvert.SerializeObject(_service.GetMoviesByPage(pageNumber, SortBy.TopRated))));
        }

        [HttpGet]
        [Route("details/{movieId}")]
        public IActionResult MovieDetails(long movieId)
        {
            return View(
                JsonConvert.DeserializeObject<MovieDetails>(
                    JsonConvert.SerializeObject(_service.GetMovieDetails(movieId))));
        }
    }
}