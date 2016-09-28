using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using WebApplication3.Extensions;
using Microsoft.AspNetCore.Http;
using static Maquom.Data.Abstract.IRepository;
using Maquom.Data;
using Maquom.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

namespace WebApplication3
{
    public class Startup
    {
        private static string _applicationPath = string.Empty;
        private static string _contentRootPath = string.Empty;

        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            _applicationPath = env.WebRootPath;
            _contentRootPath = env.ContentRootPath;
            // Setup configuration sources.

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string sqlConnectionString = Configuration["Data:MaquomConnection:ConnectionString"];
            bool useInMemoryProvider = bool.Parse(Configuration["Data:MaquomConnection:InMemoryProvider"]);

            services.AddDbContext<MaquomContext>(options =>
            {
                switch (useInMemoryProvider)
                {
                    case true:
                        // Teste bdd
                        options.UseInMemoryDatabase();
                        break;
                    default:
                        options.UseSqlServer(Configuration["Data:MaquomConnection:ConnectionString"],
                        b => b.MigrationsAssembly("Elohim.API"));
                        break;
                }
            });

            // Repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            services.AddCors();

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("back-end", policy => policy.RequireRole("back-end"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            app.UseExceptionHandler(
               builder =>
               {
                   builder.Run(
                       async context =>
                       {
                           context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                           context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                           var error = context.Features.Get<IExceptionHandlerFeature>();
                           if (error != null)
                           {
                               context.Response.AddApplicationError(error.Error.Message);
                               await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                           }
                       });
               });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Enlever le commentaire pour Web api 2
                //routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });

        }
    }
}
