using Microsoft.Graph;
using System.Collections.Generic;

namespace SnippetsApp.Models
{
    public class TeamsListDisplayModel
    {
        public IList<Group> AllTeams { get; set; }

        public IList<Group> AllNonTeamGroups { get; set; }

        public IList<Team> JoinedTeams { get; set; }

        public TeamsListDisplayModel()
        {
            AllTeams = new List<Group>();
            AllNonTeamGroups = new List<Group>();
        }
    }
}
