using GraphTutorial.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using SnippetsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Teams.Application.Responses;
using Teams.Application.Services.Abstraction;
using Teams.Client.Graph;
using Teams.Client.Models;

namespace Teams.Client.Controllers
{

    [AuthorizeForScopes(Scopes = new[] {
        GraphConstants.GroupReadWriteAll,
        GraphConstants.UserReadWriteAll })]
    public class TeamsController : BaseController
    {

        private readonly string[] teamScopes =
           new[] {
                GraphConstants.GroupReadWriteAll,
                GraphConstants.UserReadWriteAll
           };
        private readonly ITeamsManagmentService _teamsManagmentService;

        public TeamsController(ITeamsManagmentService teamsManagmentService,
            ITokenAcquisition tokenAcquisition,
            ILogger<HomeController> logger) : base(tokenAcquisition, logger)
        {
            _teamsManagmentService = teamsManagmentService;
        }


        public async Task<IActionResult> Teams()
        {

            var model = new TeamsListDisplayModel();


            var token = await _tokenAcquisition
                       .GetAccessTokenForUserAsync(teamScopes);

          var teams =  await _teamsManagmentService.GetAllTeam(token);

                return View(model);

        }


        public async Task<IActionResult> User()
        {

            var model = new TeamsListDisplayModel();


            var token = await _tokenAcquisition
                       .GetAccessTokenForUserAsync(teamScopes);

            var user =await _teamsManagmentService.GetUserData(token);



            return View(model);

        }

    }
    }

