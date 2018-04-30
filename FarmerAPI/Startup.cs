using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FarmerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace FarmerAPI
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
            services.AddMvc();

            //將原本ConnectString移到appsettings.json
            services.AddDbContext<WeatherContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MyDB")));

            //加入cross-origin-request-sharing
            services.AddCors(options=>
            {
                // BEGIN01
                options.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    builder.WithOrigins("http://example.com", "http://www.contoso.com");
                });
                // END01

                // BEGIN02
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials();
                    });
                // END02

                // BEGIN03
                options.AddPolicy("AllowSpecificMethods",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .WithMethods("GET", "POST", "HEAD");
                    });
                // END03

                // BEGIN04
                options.AddPolicy("AllowAllMethods",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .AllowAnyMethod();
                    });
                // END04

                // BEGIN05
                options.AddPolicy("AllowHeaders",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .WithHeaders("accept", "content-type", "origin", "x-custom-header");
                    });
                // END05

                // BEGIN06
                options.AddPolicy("AllowAllHeaders",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .AllowAnyHeader();
                    });
                // END06

                // BEGIN07
                options.AddPolicy("ExposeResponseHeaders",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .WithExposedHeaders("x-custom-header");
                    });
                // END07

                // BEGIN08
                options.AddPolicy("AllowCredentials",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .AllowCredentials();
                    });
                // END08

                // BEGIN09
                options.AddPolicy("SetPreflightExpiration",
                    builder =>
                    {
                        builder.WithOrigins("http://example.com")
                               .SetPreflightMaxAge(TimeSpan.FromSeconds(2520));
                    });
                // END09
            });


            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigins"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Use this method to configure the HTTP request pipeline.
            //app.UseCors("AllowAllOrigins");

            app.UseMvc();
        }
    }
}
