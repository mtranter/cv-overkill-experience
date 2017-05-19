using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expereince.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using WebApplication.Auth;

namespace WebApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(new MongoClient(Configuration.GetConnectionString("mongo")));
            services.AddScoped<IExperienceRepository>(c =>
            {
                var db = c.GetRequiredService<MongoClient>().GetDatabase("experience");
                return new MongoExperienceRepository(db);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsMyGithub", policy => policy.Requirements.Add(new JwtNameIdListing("github|3257273")));
            });

            services.AddSingleton<IAuthorizationHandler, JwtNameIdAuthorizationHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var options = new JwtBearerOptions
            {
                Audience = "k0BkgOePRLeWAr4PoIur8mz0TknuoVQr",
                Authority = "https://marktranter.eu.auth0.com/"
            };
            app.UseJwtBearerAuthentication(options);

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715
            app.UseMvc();
        }
    }
}
