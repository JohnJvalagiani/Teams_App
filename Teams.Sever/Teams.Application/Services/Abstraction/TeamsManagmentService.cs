using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Teams.Application.Responses;
using Teams.Client.Models;

namespace Teams.Application.Services.Abstraction
{
    public class TeamsManagmentService : ITeamsManagmentService
    {
        private readonly HttpClient _httpClient;

        public TeamsManagmentService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<IEnumerable<TeamResponse>> GetAllTeam(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue("Bearer", token);


            var teams = await _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/joinedTeams");

            var jsonResponseDatateams = await teams.Content.ReadAsStringAsync();
            var allteams = JsonSerializer.Deserialize<AllTeams>(jsonResponseDatateams);

            var response = new List<TeamResponse>();

            foreach (var team in allteams.value)
            {

                var res = await _httpClient.GetAsync("https://graph.microsoft.com/v1.0/groups/"+$"{team.id}"+"/members");

                var jsonResponseData = await res.Content.ReadAsStringAsync();
                var themembers = JsonSerializer.Deserialize<TeamsMembers>(jsonResponseData);

                var theteam = new TeamResponse
                {
                    teaminfo = new TeamInfo { displayName = team.displayName, description = team.description, id = team.id }
                ,
                    teamMembers = themembers
                };

                response.Add(theteam);
            }


            return response;
        }

        public async Task<UserData> GetUserData(string token)
        {

            _httpClient.DefaultRequestHeaders.Authorization =
           new AuthenticationHeaderValue("Bearer", token);


            var teams = await _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/");

            var jsonResponseDatateams = await teams.Content.ReadAsStringAsync();
            var userData = JsonSerializer.Deserialize<UserData>(jsonResponseDatateams);

            return userData;

        }


    }
}
