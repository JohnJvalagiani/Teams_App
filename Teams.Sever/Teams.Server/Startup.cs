using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Teams.Application.Services.Abstraction;
using Teams.Client.Graph;

namespace Teams.Client
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
            services.AddScoped<ITeamsManagmentService,TeamsManagmentService> ();

        services
       .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
       .AddMicrosoftIdentityWebApp(options => {
           Configuration.Bind("AzureAd", options);

           options.Prompt = "select_account";

           options.Events.OnTokenValidated = async context =>
           {
               var tokenAcquisition = context.HttpContext.RequestServices
                   .GetRequiredService<ITokenAcquisition>();

               var graphClient = new GraphServiceClient(
                   new DelegateAuthenticationProvider(async (request) =>
                   {
                       var token = await tokenAcquisition
                           .GetAccessTokenForUserAsync(GraphConstants.DefaultScopes, user: context.Principal);
                       request.Headers.Authorization =
                           new AuthenticationHeaderValue("Bearer", token);
                   })
               );

           };


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
     

      .EnableTokenAcquisitionToCallDownstreamApi(options => {
          Configuration.Bind("AzureAd", options);
      }, GraphConstants.DefaultScopes)
                .AddMicrosoftGraph(options => {
                    options.Scopes = string.Join(' ', GraphConstants.DefaultScopes);
                })
                .AddInMemoryTokenCaches();



            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
           .AddMicrosoftIdentityUI();

           




        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
