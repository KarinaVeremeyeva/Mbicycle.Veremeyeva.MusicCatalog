using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicCatalog.DataAccess;
using MusicCatalog.DataAccess.Entities;
using MusicCatalog.DataAccess.Repositories.EFRepositories;
using MusicCatalog.Services;
using MusicCatalog.Services.Interfaces;

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

            services.AddScoped<IRepository<Genre>, EFGenreRepository>();
            services.AddScoped<IRepository<Performer>, EFPerformerRepository>();
            services.AddScoped<IRepository<Album>, EFAlbumRepository>();
            services.AddScoped<IRepository<Song>, EFSongRepository>();

            services.AddScoped<IGenresService, GenresService>();
            services.AddScoped<IPerformersService, PerformersService>();
            services.AddScoped<IAlbumsService, AlbumsService>();
            services.AddScoped<ISongsService, SongsService>();

            services.AddControllers();
            services.AddMvc();
            services.AddDbContext<MusicContext>(
                options => options.UseSqlServer(connectionString)
                );
        }

        /// <summary>
        /// Specifies how the app responds to HTTP requests
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
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
            app.UseStaticFiles();
            app.UseRouting();
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
