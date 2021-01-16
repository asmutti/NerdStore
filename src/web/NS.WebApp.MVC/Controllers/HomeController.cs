using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Message = "An error has occurred! Please try again later.";
                modelErro.Title = "An wild error has occurred!";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Message =
                    "The page you requested does not exist!";
                modelErro.Title = "Ops! Página não encontrada.";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "You are not allowed to do this.";
                modelErro.Title = "Access denied";
                modelErro.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
        
    }
}