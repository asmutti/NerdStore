using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NS.WebApp.MVC.Models;

namespace NS.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        public bool ResponseHasErrors(ResponseResultModel responseResult)
        {
            return responseResult != null && responseResult.Errors.Messages.Any();
        }
    }
}