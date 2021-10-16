using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Teams.Application.HelperClases;

namespace Teams.Sever.HelperClases
{
    public class myservice
    {


        public void getToken()
        {


                 var appConfig = SettingsHelper.LoadAppSettings();

                if (appConfig == null)
                {
                    Debug.WriteLine("Missing or invalid appsettings.json...exiting");
                    return;
                }

                    var appId = appConfig["appId"];
                    var scopesString = appConfig["scopes"];
                    var scopes = scopesString.Split(';');

                    GraphHelper.Initialize(appId, scopes, (code, cancellation) => {
                        Debug.WriteLine(code.Message);
                    return Task.FromResult(0);
                });

                var accessToken = GraphHelper.GetAccessTokenAsync(scopes).Result;
    
        }
        }

}
