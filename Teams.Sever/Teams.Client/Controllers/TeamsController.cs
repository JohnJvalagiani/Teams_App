using GraphTutorial.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Client.Graph;

namespace Teams.Client.Controllers
{


    public class TeamsController:BaseController
    {

        private readonly string[] teamScopes =
           new[] {
                GraphConstants.GroupReadWriteAll,
                GraphConstants.UserReadWriteAll
           };

        public TeamsController(
            ITokenAcquisition tokenAcquisition,
            ILogger<HomeController> logger) : base(tokenAcquisition, logger)
        {
        }




    }
}
