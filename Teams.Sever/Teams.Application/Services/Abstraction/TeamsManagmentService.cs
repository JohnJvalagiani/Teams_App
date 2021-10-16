using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teams.Application.Responses;

namespace Teams.Application.Services.Abstraction
{
    public class TeamsManagmentService : ITeamsManagmentService
    {
        public Task<IEnumerable<Team>> GetAllTeam()
        {


            throw new NotImplementedException();
        }

        public Task<UserData> GetUserData()
        {
            throw new NotImplementedException();
        }
    }
}
