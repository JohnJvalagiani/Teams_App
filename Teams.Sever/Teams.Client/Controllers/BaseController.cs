using GraphTutorial.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Client.Controllers
{
    public class BaseController
    {
        protected readonly ITokenAcquisition _tokenAcquisition;
        protected readonly ILogger<HomeController> _logger;

        public BaseController(
            ITokenAcquisition tokenAcquisition,
            ILogger<HomeController> logger)
        {
            _tokenAcquisition = tokenAcquisition;
            _logger = logger;
        }

        public BaseController()
        {

        }



        protected GraphServiceClient GetGraphClientForScopes(string[] scopes)
        {
            return GraphServiceClientFactory
                .GetAuthenticatedGraphClient(async () =>
                {
                    var token = await _tokenAcquisition
                        .GetAccessTokenForUserAsync(scopes);

                    // Uncomment to print access token to debug console
                    // This will happen for every Graph call, so leave this
                    // out unless you're debugging your token
                    //_logger.LogInformation($"Access token: {token}");

                    return token;
                }
            );
        }

    }
}
