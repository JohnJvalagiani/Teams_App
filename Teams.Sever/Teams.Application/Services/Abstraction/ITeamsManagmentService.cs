using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams.Application.Services.Abstraction
{
   public interface ITeamsManagmentService
    {
        Task<IEnumerable<object>> GetAllTeam();

    }
}
