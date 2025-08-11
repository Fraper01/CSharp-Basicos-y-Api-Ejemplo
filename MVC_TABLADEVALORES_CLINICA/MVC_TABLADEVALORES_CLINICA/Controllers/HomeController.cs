using Microsoft.AspNetCore.Mvc;
using MVC_TABLADEVALORES_CLINICA.Models;
using MVC_TABLADEVALORES_CLINICA.Models.Entities;
using MVC_TABLADEVALORES_CLINICA.Services;
using System.Diagnostics;

namespace MVC_TABLADEVALORES_CLINICA.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Politicas()
        {
            return View();
        }
        public IActionResult Novedades()
        {
            return View();
        }
        public IActionResult OtraPagina()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
