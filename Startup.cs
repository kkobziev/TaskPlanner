using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Globalization;
using TaskPlanner.Services;

namespace TaskPlanner
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
            services.AddRazorPages();

            // Add localization services with absolute path to resources
            services.AddLocalization(options => 
            {
                options.ResourcesPath = "Resources";
            });
            
            // Register our custom localizer helper as a singleton
            services.AddSingleton<LocalizerHelper>();
            
            // Add MVC with localization view features
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        new LocalizerHelper();
                });

            // Configure request localization options
            var supportedCultures = new[]
            {
                new CultureInfo("en"),
                new CultureInfo("uk")
            };

            var requestLocalizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                // Add providers for determining request culture
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
                }
            };
            
            services.AddSingleton(requestLocalizationOptions);

            // Register task service with proper file path
            services.AddSingleton<ITaskService>(provider => 
                new FileTaskService(
                    provider.GetRequiredService<ILogger<FileTaskService>>(),
                    provider.GetRequiredService<IWebHostEnvironment>(),
                    "Data/tasks.json"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Use localization with the configured options
            var requestLocalizationOptions = app.ApplicationServices.GetRequiredService<RequestLocalizationOptions>();
            app.UseRequestLocalization(requestLocalizationOptions);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
