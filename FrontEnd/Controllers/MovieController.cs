using System.Diagnostics;
using FrontEnd.Models;
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
    }
}