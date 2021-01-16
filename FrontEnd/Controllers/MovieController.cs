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
            return View();
        }


        [Route("popular/{pageNumber:int}")]
        public IActionResult Popular(int pageNumber)
        {
            return View(JsonConvert.DeserializeObject<PagedMovies>(
                JsonConvert.SerializeObject(_service.GetMoviesByPage(pageNumber))));
        }

        [Route("top-rated/{pageNumber:int}")]
        public IActionResult TopRated(int pageNumber)
        {
            return View(
                JsonConvert.DeserializeObject<PagedMovies>(
                    JsonConvert.SerializeObject(_service.GetMoviesByPage(pageNumber))));
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