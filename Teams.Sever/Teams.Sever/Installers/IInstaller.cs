using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Managment_System.Server.Installers
{
    public interface IInstaller
    {

        void InstallerService(IServiceCollection serviceProviderCollection, IConfiguration configuration);
       
    }
}
