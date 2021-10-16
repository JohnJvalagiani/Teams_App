using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teams.Sever.HelperClases
{
    public static class SettingsHelper
    {

static public IConfigurationRoot LoadAppSettings()
{
    var appConfig = new ConfigurationBuilder()
        .AddUserSecrets<Program>()
        .Build();

    if (string.IsNullOrEmpty(appConfig["appId"]) ||
        string.IsNullOrEmpty(appConfig["scopes"]))
    {
        return null;
    }

    return appConfig;
}
    }
}
