using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System.Diagnostics;
using System.Threading.Tasks;
using Teams.Client.Graph;

namespace GraphTutorial.Controllers
{

    public class HomeController : Controller
    {
        ITokenAcquisition _tokenAcquisition;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ITokenAcquisition tokenAcquisition,
            ILogger<HomeController> logger)
        {
            _tokenAcquisition = tokenAcquisition;
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

     
    }
}