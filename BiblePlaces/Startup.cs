using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BiblePlaces
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // The order that middleware components are added in the Startup.Configure method defines the order in
            // which the middleware components are invoked on requests and the reverse order for the response. The
            // ORDER IS CRITICAL for security, performance, and functionality.
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/index?view=aspnetcore-2.2

            if (env.IsDevelopment())
            {
                // When the app runs in the Development environment:
                //   Use the Developer Exception Page to report app runtime errors.
                //   Use the Database Error Page to report database runtime errors.
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // When the app doesn't run in the Development environment:
                //   Enable the Exception Handler Middleware to catch exceptions thrown in the following middlewares.
                //   Use the HTTP Strict Transport Security Protocol (HSTS) Middleware.
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Return static files and end the pipeline.
            app.UseStaticFiles(new StaticFileOptions
            {
                // Specify that non-standard content types can be served, or else, you will see a 404 error.
                // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-2.2#non-standard-content-types
                ServeUnknownFileTypes = true,
            });

            // Add MVC to the request pipeline.
            app.UseMvcWithDefaultRoute();
        }
    }
}
