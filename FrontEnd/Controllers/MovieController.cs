using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Mv.Integrations;
using Newtonsoft.Json;
using RestSharp;
using Service.Interface;
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
                Popular = JsonConvert.DeserializeObject<List<MovieApi>>(
                    JsonConvert.SerializeObject(_service.GetMoviesByPage(1, SortBy.Popularity).Movies.Take(10))),
                TopRevenue = JsonConvert.DeserializeObject<List<MovieApi>>(
                    JsonConvert.SerializeObject(_service.GetMoviesByPage(1, SortBy.Revenue).Movies.Take(10))),
                Recent = JsonConvert.DeserializeObject<List<MovieApi>>(
                    JsonConvert.SerializeObject(_service.GetMoviesByPage(1, SortBy.Latest).Movies.Take(10)))
            });
        }


        [Route("popular/{pageNumber:int}")]
        public IActionResult PopularMovies(int pageNumber)
        {
            return View(JsonConvert.DeserializeObject<PagedMovies>(
                JsonConvert.SerializeObject(_service.GetMoviesByPage(pageNumber, SortBy.Popularity))));
        }

        [Route("top-rated/{pageNumber:int}")]
        public IActionResult TopRatedMovies(int pageNumber)
        {
            return View(JsonConvert.DeserializeObject<PagedMovies>(
                JsonConvert.SerializeObject(_service.GetMoviesByPage(pageNumber, SortBy.TopRated))));
        }

        [HttpGet]
        [Route("details/{movieId}")]
        public IActionResult MovieDetails(long movieId)
        {
            return View(_service.GetMovieFullDetails(movieId));
        }
        
        [HttpGet]
        [Route("favorite")]
        public IActionResult FavoriteMovies()
        {
            return View(_service.GetFavoriteMovies());
        }
        
        [HttpPost]
        [Route("add/{movieId}")]
        public HttpResponse AddToFavorite(long movieId)
        { 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _service.AddFavorite(movieId, userId);
            
            return new HttpResponse();
        }
        
        [HttpPost]
        [Route("delete/{movieId}")]
        public HttpResponse DeleteFromFavorite(long movieId)
        { 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _service.DeleteFromFavorite(movieId, userId);
            
            return new HttpResponse();
        }
    }
}