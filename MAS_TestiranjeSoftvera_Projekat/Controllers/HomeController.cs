using MAS_TestiranjeSoftvera_Projekat.DataAccessLayer.Interfaces;
using MAS_TestiranjeSoftvera_Projekat.Domain;
using MAS_TestiranjeSoftvera_Projekat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MAS_TestiranjeSoftvera_Projekat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryMesto repositoryMesto;
        private readonly IRepositoryOsoba repositoryOsoba;
        public HomeController(ILogger<HomeController> logger, IRepositoryMesto repositoryMesto, IRepositoryOsoba repositoryOsoba)
        {
            _logger = logger;
            this.repositoryMesto = repositoryMesto;
            this.repositoryOsoba = repositoryOsoba;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error", new ErrorViewModel { ExceptionMessage = "Error!" });

        }
    }
}
