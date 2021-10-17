using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teams.Client.Models;

namespace Teams.Application.Responses
{
    public class TeamResponse
    {
        public TeamInfo teaminfo { get; set; }
        public TeamsMembers teamMembers { get; set; }
    }
}
