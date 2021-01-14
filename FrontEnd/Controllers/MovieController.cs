using System.Diagnostics;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace FrontEnd.Controllers
{
    public class MovieController : Controller
    {
        private readonly IService _service;

        public MovieController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            ViewBag.Movies = _service.GetMoviesByPage(1);
            return View();
        }
        
        public IActionResult MovieDetails(long movieId)
        {
            ViewBag.Movies = _service.GetMovieDetails(movieId);
            return View();
        }
    }
}