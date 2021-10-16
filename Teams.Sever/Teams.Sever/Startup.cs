using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Managment_System.Server.Extensions;
using Task_Managment_System.Server.Installers;
using Teams.Sever.HelperClases;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Graph;
using System.Net;
using System.Net.Http.Headers;


namespace Teams.Sever
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallerServicesInAssembly(Configuration);


            services.AddControllers();

            services.AddScoped<myservice>();

            services
                        .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApp(options => {
                            Configuration.Bind("AzureAd", options);

                            options.Prompt = "select_account";

                           

                            options.Events.OnAuthenticationFailed = context => {
                                var error = WebUtility.UrlEncode(context.Exception.Message);
                                context.Response
                                    .Redirect($"/Home/ErrorWithMessage?message=Authentication+error&debug={error}");
                                context.HandleResponse();

                                return Task.FromResult(0);
                            };

                            options.Events.OnRemoteFailure = context => {
                                if (context.Failure is OpenIdConnectProtocolException)
                                {
                                    var error = WebUtility.UrlEncode(context.Failure.Message);
                                    context.Response
                                        .Redirect($"/Home/ErrorWithMessage?message=Sign+in+error&debug={error}");
                                    context.HandleResponse();
                                }

                                return Task.FromResult(0);
                            };
                        })
                        // Add ability to call web API (Graph)
                        // and get access tokens
                        .EnableTokenAcquisitionToCallDownstreamApi(options => {
                            Configuration.Bind("AzureAd", options);
                        }, GraphConstants.Scopes)
                        // Add a GraphServiceClient via dependency injection
                        .AddMicrosoftGraph(options => {
                            options.Scopes = string.Join(' ', GraphConstants.Scopes);
                        })
                        // Use in-memory token cache
                        // See https://github.com/AzureAD/microsoft-identity-web/wiki/token-cache-serialization
                        .AddInMemoryTokenCaches();

            // Require authentication
            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            // Add the Microsoft Identity UI pages for signin/out
            .AddMicrosoftIdentityUI();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task_Managment_System v1"));
            }

            app.ConfigurationCustomExceptionMiddleware();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
