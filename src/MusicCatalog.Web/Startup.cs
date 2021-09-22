using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using MusicCatalog.Web.Mappings;
using MusicCatalog.Web.Services;
using MusicCatalog.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MusicCatalog.Web
{
    /// <summary>
    /// Configures services and the app's request pipeline
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration of the app
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Registers services for the app
        /// </summary>
        /// <param name="services">Services collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var uriString = Configuration.GetSection("UriSettings:WebApiUri").Value;

            var mapping = new MapperConfiguration(
                map =>
                {
                    map.AddProfile<BusinessLogicProfile>();
                    map.AddProfile<WebProfile>();
                    map.AddProfile<AccountProfile>();
                });

            services.AddSingleton(mapping.CreateMapper());

            services.AddHttpClient<IUserApiService, UserApiService>(client =>
            {
                client.BaseAddress = new Uri(uriString);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient<IAlbumApiService, AlbumApiService>(client =>
            {
                client.BaseAddress = new Uri(uriString);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient<IGenreApiService, GenreApiService>(client =>
            {
                client.BaseAddress = new Uri(uriString);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddHttpClient<IPerformerApiService, PerformerApiService>(client =>
            {
                client.BaseAddress = new Uri(uriString);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie(JwtBearerDefaults.AuthenticationScheme,
                options => 
                {
                    options.LoginPath = "/Accounts/Login";
                    options.AccessDeniedPath = "/Accounts/Login";
                });

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
                options => options.UseSqlServer(connectionString));
        }

        /// <summary>
        /// Specifies how the app responds to HTTP requests
        /// </summary>
        /// <param name="app">Application</param>
        /// <param name="env">Hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

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
