using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using SoldierInfo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace SoldierInfo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(
            IServiceCollection services
            )
        {
            var connectionString = Configuration.GetConnectionString("SoldierPgSqlConnection");

            services.AddDbContext<SoldierContext>(
                options => options.UseNpgsql(connectionString)
                );
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v0.8", new OpenApiInfo { Title = "Soldier Info API", Version = "v0.8" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var SwaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(SwaggerOptions);

            app.UseSwagger(option =>
            {
                option.RouteTemplate = SwaggerOptions.JsonRoute;
            });

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(SwaggerOptions.UiEndpoint, SwaggerOptions.Description);
                option.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Soldiers Info API");
                });

                endpoints.MapControllers();
            });
        }
    }
}
