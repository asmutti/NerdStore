using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        public bool ResponseHasErrors(ResponseResultModel responseResult)
        {
            if (responseResult == null || !responseResult.Errors.Messages.Any()) 
                return false;
            
            foreach (var message in responseResult.Errors.Messages)
            {
                ModelState.AddModelError(string.Empty, message);
            }

            return true;

        }
    }
}