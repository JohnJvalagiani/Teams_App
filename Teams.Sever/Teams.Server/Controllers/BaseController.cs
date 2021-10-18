using GraphTutorial.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teams.Client.Graph;

namespace Teams.Client.Controllers
{
    public class BaseController: Controller
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


        protected async Task<List<T>> GetAllPages<T>(
          GraphServiceClient graphClient,
          ICollectionPage<T> page)
        {
            var allItems = new List<T>();

            var pageIterator = PageIterator<T>.CreatePageIterator(
                graphClient, page,
                (item) => {
                    // This code executes for each item in the
                    // collection
                    allItems.Add(item);
                    return true;
                }
            );

            await pageIterator.IterateAsync();

            return allItems;
        }


        protected GraphServiceClient GetGraphClientForScopes(string[] scopes)
        {
            return GraphServiceClientFactory
                .GetAuthenticatedGraphClient(async () =>
                {
                    var token = await _tokenAcquisition
                        .GetAccessTokenForUserAsync(scopes);

                   
                  //  _logger.LogInformation($"Access token: {token}");

                    return token;
                }
            );
        }

    }
}
