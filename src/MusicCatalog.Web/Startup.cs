using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MusicCatalog.BusinessLogic;
using MusicCatalog.BusinessLogic.Interfaces;
using MusicCatalog.BusinessLogic.Services;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories.EFRepositories;
using MusicCatalog.Web.Models;
using System.Collections.Generic;
using System.Globalization;

namespace MusicCatalog.Web
{
    /// <summary>
    /// Configures services and the app's request pipeline
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Registers services for the app
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var mapping = new MapperConfiguration(
                map =>
                {
                    map.AddProfile<BusinessLogicProfile>();
                    map.AddProfile<WebProfile>();
                });

            services.AddSingleton(mapping.CreateMapper());

            services.AddScoped<IRepository<Genre>, EFGenreRepository>();
            services.AddScoped<IRepository<Performer>, EFPerformerRepository>();
            services.AddScoped<IRepository<Album>, EFAlbumRepository>();
            services.AddScoped<IRepository<Song>, EFSongRepository>();

            services.AddScoped<IGenresService, GenresService>();
            services.AddScoped<IPerformersService, PerformersService>();
            services.AddScoped<IAlbumsService, AlbumsService>();
            services.AddScoped<ISongsService, SongsService>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("ru"),
                    new CultureInfo("en")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "ru", uiCulture: "ru");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                };
            });

            services.AddDbContext<MusicContext>(
                options => options.UseSqlServer(connectionString)
                );

            services.AddDbContext<ApplicationContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"))
                );
            services.AddIdentity<User, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationContext>();
        }

        /// <summary>
        /// Specifies how the app responds to HTTP requests
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/Error");
            app.UseStatusCodePages("text/plain", "Error. Status code : {0}");
            
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
