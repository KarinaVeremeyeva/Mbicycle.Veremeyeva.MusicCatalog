using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MusicCatalog.WebApi.JwtTokenAuth;
using MusicCatalog.WebApi.Mappings;
using MusicCatalog.WebApi.Services;
using System;

namespace MusicCatalog.WebApi
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
            var uriString = Configuration.GetSection("UriSettings:IdentityApiUri").Value;

            var mapping = new MapperConfiguration(map => map.AddProfile<UserProfile>());
            services.AddSingleton(mapping.CreateMapper());

            services.AddHttpClient<IAccountApiService, AccountApiService>(client =>
            {
                client.BaseAddress = new Uri(uriString);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            services.AddAuthentication(JwtAutheticationConstants.SchemeName)
                .AddScheme<JwtAuthenticationOptions, JwtAuthenticationHandler>(
                JwtAutheticationConstants.SchemeName, null);

            services.AddControllers();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MusicCatalog.WebApi",
                    Version = "v1"
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "Jwt Token is required to access the endpoints",
                    In = ParameterLocation.Header,
                    Name = "JWT Authentication",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                swagger.AddSecurityDefinition("Bearer", jwtSecurityScheme);
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        jwtSecurityScheme,
                        Array.Empty<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Specifies how the app responds to HTTP requests
        /// </summary>
        /// <param name="app">Application</param>
        /// <param name="env">Hosting environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicCatalog.WebApi v1");
                });
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
